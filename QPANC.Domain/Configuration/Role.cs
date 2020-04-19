using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QPANC.Domain.Configuration
{
    public class Role : IEntityTypeConfiguration<Identity.Role>
    {
        public void Configure(EntityTypeBuilder<Identity.Role> entity)
        {
            entity.HasMany(x => x.Users)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(x => x.Claims)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
