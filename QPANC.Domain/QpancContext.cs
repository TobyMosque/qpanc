using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using QPANC.Domain.Extensions;
using QPANC.Services.Abstract;
using QPANC.Services.Abstract.Entities.Enums;
using QPANC.Services.Abstract.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Identity = QPANC.Services.Abstract.Entities.Identity;

namespace QPANC.Domain
{
    public class QpancContext : IdentityDbContext<Identity.User, Identity.Role, Guid, Identity.UserClaim, Identity.UserRole, Identity.UserLogin, Identity.RoleClaim, Identity.UserToken>
    {
        private class AuditMapProp
        {
            internal PropertyInfo Base { get; set; }
            internal PropertyInfo Audit { get; set; }
        }

        private class AuditMapper
        {
            internal Type AuditType { get; set; }
            internal ICollection<AuditMapProp> Props { get; set; }

            internal IAudit CreateAudit<TEntity>(TEntity entity) where TEntity : IEntity
            {
                var audit = (IAudit)Activator.CreateInstance(this.AuditType);
                foreach (var pair in this.Props)
                {
                    pair.Audit.SetValue(audit, pair.Base.GetValue(entity));
                }
                return audit;
            }
        }

        private static Dictionary<Type, AuditMapper> AuditTypes;

        static QpancContext()
        {
            var baseTypes = typeof(QpancContext).GetProperties()
                .Where(x => x.PropertyType.IsGenericType
                    && typeof(IEntity).IsAssignableFrom(x.PropertyType.GenericTypeArguments[0]))
                .Select(x => x.PropertyType.GenericTypeArguments[0]);

            var auditTypes = typeof(Audit.QpancAuditContext).GetProperties()
                .Where(x => x.PropertyType.IsGenericType
                    && typeof(IAudit).IsAssignableFrom(x.PropertyType.GenericTypeArguments[0]))
                .Select(x => x.PropertyType.GenericTypeArguments[0]);

            var mapTypes = (
                from baseType in baseTypes
                from auditType in auditTypes
                where baseType.BaseType.IsAssignableFrom(auditType)
                select (baseType, auditType));

            AuditTypes = new Dictionary<Type, AuditMapper>();
            foreach (var (baseType, auditType) in mapTypes)
            {
                var baseProps = baseType.GetProperties();
                var auditProps = auditType.GetProperties();

                var props = (
                    from baseProp in baseType.GetProperties()
                    join auditProp in auditType.GetProperties() on baseProp.Name equals auditProp.Name
                    select new AuditMapProp { Base = baseProp, Audit = auditProp }).ToList();

                AuditTypes.Add(baseType, new AuditMapper { AuditType = auditType, Props = props });
            }
        }

        private IServiceProvider _service;
        private IConnectionStrings _connStrings;

        public QpancContext(IServiceProvider service, IConnectionStrings connStrings)
        {
            this._service = service;
            this._connStrings = connStrings;
        }

        public QpancContext(DbContextOptions<QpancContext> options, IServiceProvider service, IConnectionStrings connStrings) : base(options)
        {
            this._service = service;
            this._connStrings = connStrings;
        }

        public DbSet<Identity.Session> Sessions { get; set; }

        #region EntityFrameworkCore.Triggers extensions
        public override Int32 SaveChanges()
        {
            return this.SaveChangesWithTriggers(base.SaveChanges, acceptAllChangesOnSuccess: true);
        }
        public override Int32 SaveChanges(Boolean acceptAllChangesOnSuccess)
        {
            return this.SaveChangesWithTriggers(base.SaveChanges, acceptAllChangesOnSuccess);
        }
        public override Task<Int32> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, acceptAllChangesOnSuccess: true, cancellationToken: cancellationToken);
        }
        public override Task<Int32> SaveChangesAsync(Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, acceptAllChangesOnSuccess, cancellationToken);
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.ApplyConfiguration

            modelBuilder.ApplyConfiguration(new Configuration.User());
            modelBuilder.ApplyConfiguration(new Configuration.Role());

            modelBuilder.Entities<IEntity>(this, nameof(this.ModelEntity));
        }

        private void Audit<TEntity, TAudit>(
            QpancContext context,
            TEntity entity,
            TAudit auditEntity,
            ICollection<PropertyInfo> primaryKeys,
            AuditOperation operation,
            DateTimeOffset auditedAt) where TEntity : class, IEntity where TAudit : class, IAudit
        {
            var auditContext = context._service.GetRequiredService<Audit.QpancAuditContext>();
            var dbSet = auditContext.Set<TAudit>();

            var condition = default(BinaryExpression);
            var expItem = Expression.Parameter(typeof(TAudit), "audit");
            foreach (var primaryKey in primaryKeys)
            {
                var expProperty = Expression.Property(expItem, primaryKey.Name);
                var expValue = Expression.Constant(primaryKey.GetValue(auditEntity));
                var expEqual = Expression.Equal(expProperty, expValue);
                condition = condition == default ? expEqual : Expression.And(condition, expEqual);
            }
            var expLambda = Expression.Lambda<Func<TAudit, bool>>(condition, expItem);
            var revQuery = dbSet.Where(expLambda);
            var revNumber = dbSet.Where(expLambda)
                .OrderByDescending(x => x.RevNumber)
                .Select(x => x.RevNumber)
                .FirstOrDefault();
            revNumber = revNumber == default ? 1 : revNumber + 1;

            auditEntity.RevNumber = revNumber;
            auditEntity.UserSessionId = entity.UserSessionId;
            auditEntity.Revision = entity.Revision;
            auditEntity.AuditedAt = auditedAt;
            auditEntity.Operation = operation;

            auditContext.Add(auditEntity);
            auditContext.SaveChanges();
        }

        private void ModelEntity<TEntity>(EntityTypeBuilder<TEntity> entity) where TEntity : class, IEntity
        {
            var keys = entity.Metadata.FindPrimaryKey().Properties.Select(p => p.Name).ToList();
            keys.Insert(0, "IsDeleted");
            entity.HasIndex(keys.ToArray()).IsUnique();

            entity.HasQueryFilter(x => !x.IsDeleted);
            entity.Property(x => x.DeletedAt).HasColumnType("timestamp with time zone");
            entity.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone");
            entity.Property(x => x.UpsertedAt).HasColumnType("timestamp with time zone");

            var baseType = entity.Metadata.ClrType;
            AuditTypes.TryGetValue(baseType, out var mapper);

            if (mapper != default)
            {
                var primaryKeys = mapper.Props.Select(x => x.Audit).Where(x => keys.Contains(x.Name)).ToList();
                var auditMethod = typeof(QpancContext)
                    .GetMethod(nameof(QpancContext.Audit), BindingFlags.Instance | BindingFlags.NonPublic)
                    .MakeGenericMethod(baseType, mapper.AuditType);

                Action<QpancContext, IEntity, AuditOperation, DateTimeOffset> auditEntity = (context, entity, operation, auditedAt) =>
                {
                    var auditEntity = mapper.CreateAudit(entity);
                    auditMethod.Invoke(context, new object[] { context, entity, auditEntity, primaryKeys, operation, auditedAt });
                };
                Triggers<TEntity>.Inserted += entry =>
                {
                    auditEntity(entry.Context as QpancContext, entry.Entity, AuditOperation.Create, entry.Entity.CreatedAt);
                };

                Triggers<TEntity>.Updated += entry =>
                {
                    auditEntity(entry.Context as QpancContext, entry.Entity, AuditOperation.Update, entry.Entity.UpsertedAt.Value);
                };

                Triggers<TEntity>.Deleted += entry =>
                {
                    auditEntity(entry.Context as QpancContext, entry.Entity, AuditOperation.Delete, entry.Entity.DeletedAt.Value);
                };
            }
            
            Func<QpancContext, (ILoggedUser loggedUser, RT.Comb.ICombProvider comb) > getServices = (context) =>
            {
                var loggedUser = context._service.GetRequiredService<ILoggedUser>();
                var comb = context._service.GetRequiredService<RT.Comb.ICombProvider>();

                return (loggedUser, comb);
            };

            Triggers<TEntity>.Inserting += entry =>
            {
                var (loggedUser, comb) = getServices(entry.Context as QpancContext);

                entry.Entity.UserSessionId = loggedUser.SessionId;
                entry.Entity.Revision = comb.Create();
                entry.Entity.CreatedAt = DateTimeOffset.Now;
                entry.Entity.UpsertedAt = DateTimeOffset.Now;
                entry.Entity.IsDeleted = false;
            };

            Triggers<TEntity>.Updating += entry =>
            {
                var (loggedUser, comb) = getServices(entry.Context as QpancContext);

                entry.Entity.UserSessionId = loggedUser.SessionId;
                entry.Entity.Revision = comb.Create();
                entry.Entity.UserSessionId = loggedUser.SessionId;
                entry.Entity.UpsertedAt = DateTimeOffset.Now;
            };

            Triggers<TEntity>.Deleting += entry =>
            {
                var (loggedUser, comb) = getServices(entry.Context as QpancContext);

                entry.Cancel = true;
                entry.Entity.UserSessionId = loggedUser.SessionId;
                entry.Entity.Revision = comb.Create();
                entry.Entity.UserSessionId = loggedUser.SessionId;
                entry.Entity.DeletedAt = DateTimeOffset.Now;
                entry.Entity.IsDeleted = true;
            };
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(this._connStrings.DefaultConnection, options => {
                    options.MigrationsAssembly("QPANC.Domain");
                });
            }
        }
    }
}
