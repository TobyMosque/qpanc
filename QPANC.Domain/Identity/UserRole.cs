using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QPANC.Domain.Identity
{
    public class UserRole : IdentityUserRole<Guid>, Interfaces.IEntity
    {
        #region IEntity interface
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public DateTimeOffset? UpsertedAt { get; set; }
        public Guid? UserSessionId { get; set; }
        #endregion

        [ForeignKey(nameof(UserRole.UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(UserRole.RoleId))]
        public Role Role { get; set; }
    }
}
