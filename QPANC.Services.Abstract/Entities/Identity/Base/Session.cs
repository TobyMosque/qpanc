using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QPANC.Services.Abstract.Entities.Identity.Base
{
    public class Session
    {
        public Guid SessionId { get; set; }

        public Guid UserId { get; set; }

        public DateTimeOffset ExpireAt { get; set; }
    }
}
