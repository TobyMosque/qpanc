using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QPANC.Domain.Identity;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QPANC.Api.Middlewares
{
    public class SwaggerBasicAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public SwaggerBasicAuthMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<User> userManager)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                var isAuthorized = false;
                string authorization = context.Request.Headers["Authorization"];
                if (authorization != default && authorization.StartsWith("Basic "))
                {
                    var encoded = authorization.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();

                    var binary = Convert.FromBase64String(encoded);
                    var decoded = Encoding.UTF8.GetString(binary).Split(':', 2);
                    var username = decoded[0];
                    var password = decoded[1];
                    var user = await userManager.FindByNameAsync(username + "@qpanc.app");
                    if (user != default)
                    {
                        var isAuthenticated = await userManager.CheckPasswordAsync(user, password);
                        if (isAuthenticated)
                        {
                            isAuthorized = await userManager.IsInRoleAsync(user, "Developer");
                        }
                    }
                }
                if (isAuthorized)
                {
                    await this._next.Invoke(context);
                }
                else
                {
                    context.Response.Headers["WWW-Authenticate"] = "Basic";
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
            }
            else
            {
                await this._next.Invoke(context);
            }
        }
    }
}
