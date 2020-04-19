using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using QPANC.Services.Abstract;
using System;
using System.Linq;
using System.Security.Claims;

namespace QPANC.Api.Services
{
    public class LoggedUser : ILoggedUser
    {
        private IHttpContextAccessor _context;
        public LoggedUser(IHttpContextAccessor context)
        {
            this._context = context;
            var isAuthenticated = this._context.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
            if (isAuthenticated)
            {
                var sub = this._context.HttpContext.User.Claims
                        .Where(x => x.Type == ClaimTypes.NameIdentifier)
                        .Select(x => x.Value)
                        .FirstOrDefault();
                var jti = this._context.HttpContext.User.Claims
                        .Where(x => x.Type == JwtRegisteredClaimNames.Jti)
                        .Select(x => x.Value)
                        .FirstOrDefault();
                if (sub != default)
                {
                    this.UserId = Guid.Parse(sub);
                }
                if (jti != default)
                {
                    this.SessionId = Guid.Parse(jti);
                }
            }
        }

        public Guid? SessionId { get; private set; }
        public Guid? UserId { get; private set; }
    }
}
