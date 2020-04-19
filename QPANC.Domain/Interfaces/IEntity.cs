using System;

namespace QPANC.Domain.Interfaces
{
    public interface IEntity
    {
        bool IsDeleted { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset? UpsertedAt { get; set; }
        DateTimeOffset? DeletedAt { get; set; }
        Guid? UserSessionId { get; set; }
    }
}
