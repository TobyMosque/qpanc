using QPANC.Services.Abstract.Entities.Enums;
using System;

namespace QPANC.Services.Abstract.Entities.Interfaces
{
    public interface IAudit
    {
        int RevNumber { get; set; }
        Guid Revision { get; set; }
        AuditOperation Operation { get; set; }
        DateTimeOffset AuditedAt { get; set; }
        Guid? UserSessionId { get; set; }
    }
}
