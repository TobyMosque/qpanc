using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QPANC.Services.Abstract.Entities.Identity.Base
{
    public class User : IdentityUser<Guid>
    {
        public User() { }
        public User(string userName) : base(userName) { }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
