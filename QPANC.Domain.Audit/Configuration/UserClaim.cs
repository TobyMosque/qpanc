using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model = QPANC.Services.Abstract.Entities.Identity.Audit;

namespace QPANC.Domain.Audit.Configuration
{
    public class UserClaim : IEntityTypeConfiguration<Model.UserClaim>
    {
        public void Configure(EntityTypeBuilder<Model.UserClaim> entity)
        {
            entity.ToTable("AspNetUserClaims");
            entity.HasIndex(x => new { x.Id, x.RevNumber });
        }
    }
}
