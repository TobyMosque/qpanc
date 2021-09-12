using System;

namespace QPANC.Services.Abstract.Entities.Interfaces
{
    public interface IEntity
    {
        bool IsDeleted { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset? UpsertedAt { get; set; }
        DateTimeOffset? DeletedAt { get; set; }
        Guid? UserSessionId { get; set; }
        Guid Revision { get; set; }
    }
}
