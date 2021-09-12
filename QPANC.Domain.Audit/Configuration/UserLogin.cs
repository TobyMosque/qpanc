using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model = QPANC.Services.Abstract.Entities.Identity.Audit;

namespace QPANC.Domain.Audit.Configuration
{
    public class UserLogin : IEntityTypeConfiguration<Model.UserLogin>
    {
        public void Configure(EntityTypeBuilder<Model.UserLogin> entity)
        {
            entity.ToTable("AspNetUserLogins");
            entity.HasIndex(x => new { x.LoginProvider, x.ProviderKey, x.RevNumber });
        }
    }
}
