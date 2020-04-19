using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPANC.Domain.Extensions;
using QPANC.Domain.Interfaces;
using QPANC.Services.Abstract;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QPANC.Domain
{
    public class QpancContext : IdentityDbContext<Identity.User, Identity.Role, Guid, Identity.UserClaim, Identity.UserRole, Identity.UserLogin, Identity.RoleClaim, Identity.UserToken>
    {
        private IConnectionStrings _connStrings;
        private ILoggedUser _loggedUser;

        public QpancContext(IConnectionStrings connStrings, ILoggedUser loggedUser)
        {
            this._connStrings = connStrings;
            this._loggedUser = loggedUser;
        }

        public QpancContext(DbContextOptions<QpancContext> options, IConnectionStrings connStrings, ILoggedUser loggedUser) : base(options)
        {
            this._connStrings = connStrings;
            this._loggedUser = loggedUser;
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

        private void ModelEntity<TEntity>(EntityTypeBuilder<TEntity> entity) where TEntity : class, IEntity
        {
            var keys = entity.Metadata.FindPrimaryKey().Properties.Select(p => p.Name).ToList();
            keys.Insert(0, "IsDeleted");
            entity.HasIndex(keys.ToArray()).IsUnique();

            entity.HasQueryFilter(x => !x.IsDeleted);
            entity.Property(x => x.DeletedAt).HasColumnType("timestamp with time zone");
            entity.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone");
            entity.Property(x => x.UpsertedAt).HasColumnType("timestamp with time zone");

            Triggers<TEntity>.Inserting += entry =>
            {
                entry.Entity.UserSessionId = this._loggedUser.SessionId;
                entry.Entity.CreatedAt = DateTimeOffset.Now;
                entry.Entity.UpsertedAt = DateTimeOffset.Now;
                entry.Entity.IsDeleted = false;
            };

            Triggers<TEntity>.Updating += entry =>
            {
                entry.Entity.UserSessionId = this._loggedUser.SessionId;
                entry.Entity.UpsertedAt = DateTimeOffset.Now;
            };

            Triggers<TEntity>.Deleting += entry =>
            {
                entry.Cancel = true;
                entry.Entity.UserSessionId = this._loggedUser.SessionId;
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
