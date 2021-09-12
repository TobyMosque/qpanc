using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model = QPANC.Services.Abstract.Entities.Identity.Audit;

namespace QPANC.Domain.Audit.Configuration
{
    public class User : IEntityTypeConfiguration<Model.User>
    {
        public void Configure(EntityTypeBuilder<Model.User> entity)
        {
            entity.HasIndex(x => new { x.Id, x.RevNumber });

            entity.ToTable("AspNetUsers");
            entity.Property(x => x.UserName).HasMaxLength(256);
            entity.Property(x => x.NormalizedUserName).HasMaxLength(256);
            entity.Property(x => x.Email).HasMaxLength(256);
            entity.Property(x => x.NormalizedEmail).HasMaxLength(256);
            entity.Property(u => u.UserName).HasMaxLength(256);
        }
    }
}
