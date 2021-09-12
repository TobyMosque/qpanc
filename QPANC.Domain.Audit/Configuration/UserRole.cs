using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model = QPANC.Services.Abstract.Entities.Identity.Audit;

namespace QPANC.Domain.Audit.Configuration
{
    public class UserRole : IEntityTypeConfiguration<Model.UserRole>
    {
        public void Configure(EntityTypeBuilder<Model.UserRole> entity)
        {
            entity.ToTable("AspNetUserRoles");
            entity.HasIndex(x => new { x.UserId, x.RoleId, x.RevNumber });
        }
    }
}
