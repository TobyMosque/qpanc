using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model = QPANC.Services.Abstract.Entities.Identity.Audit;

namespace QPANC.Domain.Audit.Configuration
{
    public class Session : IEntityTypeConfiguration<Model.Session>
    {
        public void Configure(EntityTypeBuilder<Model.Session> entity)
        {
            entity.HasIndex(x => new { x.SessionId, x.RevNumber });
        }
    }
}
