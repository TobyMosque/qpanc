using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QPANC.Services.Abstract.Entities.Identity.Base
{
    public class UserLogin : IdentityUserLogin<Guid>
    {
    }
}
