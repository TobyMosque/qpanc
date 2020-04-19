using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QPANC.Domain.Identity
{
    public class User : IdentityUser<Guid>, Interfaces.IEntity
    {
        public User() { }
        public User(string userName) : base(userName) { }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        #region IEntity interface
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public DateTimeOffset? UpsertedAt { get; set; }
        public Guid? UserSessionId { get; set; }
        #endregion

        [InverseProperty(nameof(Session.User))]
        public ICollection<Session> Sessions { get; set; }

        [InverseProperty(nameof(UserRole.User))]
        public ICollection<UserRole> Roles { get; set; }

        [InverseProperty(nameof(UserLogin.User))]
        public ICollection<UserLogin> Logins { get; set; }

        [InverseProperty(nameof(UserClaim.User))]
        public ICollection<UserClaim> Claims { get; set; }

        [InverseProperty(nameof(UserToken.User))]
        public ICollection<UserToken> Tokens { get; set; }
    }
}
