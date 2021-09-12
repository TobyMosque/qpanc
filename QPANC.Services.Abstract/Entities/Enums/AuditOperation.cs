using System;

namespace QPANC.Services.Abstract.Entities.Enums
{
    public enum AuditOperation: byte
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }
}
