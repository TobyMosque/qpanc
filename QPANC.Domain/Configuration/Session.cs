using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Identity = QPANC.Services.Abstract.Entities.Identity;

namespace QPANC.Domain.Configuration
{
    public class Session : IEntityTypeConfiguration<Identity.Session>
    {
        public void Configure(EntityTypeBuilder<Identity.Session> entity)
        {
            entity.HasKey(x => x.SessionId);
        }
    }
}
