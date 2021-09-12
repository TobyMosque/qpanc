using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model = QPANC.Services.Abstract.Entities.Identity.Audit;

namespace QPANC.Domain.Audit.Configuration
{
    public class Role : IEntityTypeConfiguration<Model.Role>
    {
        public void Configure(EntityTypeBuilder<Model.Role> entity)
        {
            entity.HasIndex(x => new { x.Id, x.RevNumber });

            entity.ToTable("AspNetRoles");
            entity.Property(x => x.Name).HasMaxLength(256);
            entity.Property(x => x.NormalizedName).HasMaxLength(256);
        }
    }
}
