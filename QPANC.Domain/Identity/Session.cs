using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QPANC.Domain.Identity
{
    public class Session : Interfaces.IEntity
    {
        [Key]
        public Guid SessionId { get; set; }

        public Guid UserId { get; set; }

        public DateTimeOffset ExpireAt { get; set; }

        #region IEntity interface
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public DateTimeOffset? UpsertedAt { get; set; }
        public Guid? UserSessionId { get; set; }
        #endregion

        [ForeignKey(nameof(Session.UserId))]
        public User User { get; set; }
    }
}
