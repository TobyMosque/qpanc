using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace QPANC.Services.Abstract.Entities.Identity.Base
{
    public class Role : IdentityRole<Guid>
    {
        public Role() { }
        public Role(string roleName) : base(roleName) { }
    }
}
