using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QPANC.Services.Abstract.Entities.Identity
{
    public class User : Base.User, Interfaces.IEntity
    {
        #region IEntity interface
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public DateTimeOffset? UpsertedAt { get; set; }
        public Guid? UserSessionId { get; set; }
        public Guid Revision { get; set; }
        #endregion

        public ICollection<Session> Sessions { get; set; }

        public ICollection<UserRole> Roles { get; set; }

        public ICollection<UserLogin> Logins { get; set; }

        public ICollection<UserClaim> Claims { get; set; }

        public ICollection<UserToken> Tokens { get; set; }
    }
}
