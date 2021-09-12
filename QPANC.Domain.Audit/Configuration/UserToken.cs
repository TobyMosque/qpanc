using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model = QPANC.Services.Abstract.Entities.Identity.Audit;

namespace QPANC.Domain.Audit.Configuration
{
    public class UserToken : IEntityTypeConfiguration<Model.UserToken>
    {
        public void Configure(EntityTypeBuilder<Model.UserToken> entity)
        {
            entity.ToTable("AspNetUserTokens");
            entity.HasIndex(x => new { x.UserId, x.LoginProvider, x.RevNumber });
        }
    }
}
