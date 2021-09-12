using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QPANC.Services.Abstract.Entities.Identity
{
    public class Role : Base.Role, Interfaces.IEntity
    {
        #region IEntity interface
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public DateTimeOffset? UpsertedAt { get; set; }
        public Guid? UserSessionId { get; set; }
        public Guid Revision { get; set; }
        #endregion

        public ICollection<UserRole> Users { get; set; }

        public ICollection<RoleClaim> Claims { get; set; }
    }
}
