using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPANC.Domain.Extensions;
using QPANC.Services.Abstract;
using QPANC.Services.Abstract.Entities.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Model = QPANC.Services.Abstract.Entities.Identity.Audit;

namespace QPANC.Domain.Audit
{
    public class QpancAuditContext : DbContext
    {
        private IConnectionStrings _connStrings;

        public QpancAuditContext(IConnectionStrings connStrings)
        {
            this._connStrings = connStrings;
        }

        public QpancAuditContext(DbContextOptions<QpancAuditContext> options, IConnectionStrings connStrings) : base(options)
        {
            this._connStrings = connStrings;
        }

        // <Identity.User, Identity.Role, Guid, Identity.UserClaim, Identity.UserRole, Identity.UserLogin, Identity.RoleClaim, Identity.UserToken>

        public DbSet<Model.User> Users { get; set; }
        public DbSet<Model.Role> Roles { get; set; }
        public DbSet<Model.UserClaim> UserClaims { get; set; }
        public DbSet<Model.UserRole> UserRoles { get; set; }
        public DbSet<Model.UserLogin> UserLogins { get; set; }
        public DbSet<Model.RoleClaim> RoleClaims { get; set; }
        public DbSet<Model.UserToken> UserTokens { get; set; }
        public DbSet<Model.Session> Sessions { get; set; }

        /*
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
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new Configuration.User());
            modelBuilder.ApplyConfiguration(new Configuration.Role());
            modelBuilder.ApplyConfiguration(new Configuration.UserClaim());
            modelBuilder.ApplyConfiguration(new Configuration.UserRole());
            modelBuilder.ApplyConfiguration(new Configuration.UserLogin());
            modelBuilder.ApplyConfiguration(new Configuration.RoleClaim());
            modelBuilder.ApplyConfiguration(new Configuration.UserToken());
            modelBuilder.ApplyConfiguration(new Configuration.Session());

            modelBuilder.Entities<IAudit>(this, nameof(this.ModelEntity));
        }

        private void ModelEntity<TAudit>(EntityTypeBuilder<TAudit> entity) where TAudit : class, IAudit
        {
            entity.HasKey(x => x.Revision);
            entity.Property(x => x.AuditedAt).HasColumnType("timestamp with time zone");

            /*
            Func<IServiceProvider, (ILoggedUser loggedUser, RT.Comb.ICombProvider comb)> getServices = service =>
            {
                var loggedUser = service.GetRequiredService<ILoggedUser>();
                var comb = service.GetRequiredService<RT.Comb.ICombProvider>();
                return (loggedUser, comb);
            };

            Triggers<TEntity>.Inserting += entry =>
            {
                var (loggedUser, comb) = getServices(entry.Service);

                entry.Entity.UserSessionId = loggedUser.SessionId;
                entry.Entity.CreatedAt = DateTimeOffset.Now;
                entry.Entity.UpsertedAt = DateTimeOffset.Now;
                entry.Entity.IsDeleted = false;
            };

            Triggers<TEntity>.Updating += entry =>
            {
                var (loggedUser, comb) = getServices(entry.Service);

                entry.Entity.UserSessionId = loggedUser.SessionId;
                entry.Entity.UpsertedAt = DateTimeOffset.Now;
            };

            Triggers<TEntity>.Deleting += entry =>
            {
                var (loggedUser, comb) = getServices(entry.Service);

                entry.Cancel = true;
                entry.Entity.UserSessionId = loggedUser.SessionId;
                entry.Entity.DeletedAt = DateTimeOffset.Now;
                entry.Entity.IsDeleted = true;
            };
            */
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(this._connStrings.AuditConnection, options => {
                    options.MigrationsAssembly("QPANC.Domain.Audit");
                });
            }
        }
    }
}
