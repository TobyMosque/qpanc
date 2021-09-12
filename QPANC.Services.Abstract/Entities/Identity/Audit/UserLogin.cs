using QPANC.Services.Abstract.Entities.Enums;
using System;

namespace QPANC.Services.Abstract.Entities.Identity.Audit
{
    public class UserLogin : Base.UserLogin, Interfaces.IAudit
    {
        #region IAudit interface
        public int RevNumber { get; set; }
        public Guid Revision { get; set; }
        public AuditOperation Operation { get; set; }
        public DateTimeOffset AuditedAt { get; set; }
        public Guid? UserSessionId { get; set; }
        #endregion
    }
}
