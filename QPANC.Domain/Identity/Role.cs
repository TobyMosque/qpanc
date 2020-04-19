using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QPANC.Domain.Identity
{
    public class Role : IdentityRole<Guid>, Interfaces.IEntity
    {
        public Role() { }
        public Role(string roleName) : base(roleName) { }

        #region IEntity interface
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public DateTimeOffset? UpsertedAt { get; set; }
        public Guid? UserSessionId { get; set; }
        #endregion

        [InverseProperty(nameof(UserRole.Role))]
        public ICollection<UserRole> Users { get; set; }

        [InverseProperty(nameof(RoleClaim.Role))]
        public ICollection<RoleClaim> Claims { get; set; }
    }
}
