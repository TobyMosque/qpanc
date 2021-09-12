using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model = QPANC.Services.Abstract.Entities.Identity.Audit;

namespace QPANC.Domain.Audit.Configuration
{
    public class RoleClaim : IEntityTypeConfiguration<Model.RoleClaim>
    {
        public void Configure(EntityTypeBuilder<Model.RoleClaim> entity)
        {
            entity.ToTable("AspNetRoleClaims");
            entity.HasIndex(x => new { x.Id, x.RevNumber });
        }
    }
}
