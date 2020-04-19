using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QPANC.Domain;
using QPANC.Services.Abstract;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace QPANC.Api.Options
{
    public class JwtBearer : IConfigureOptions<JwtBearerOptions>
    {
        private IServiceProvider _provider;
        private IJwtBearer _settings;

        public JwtBearer(IServiceProvider provider, IJwtBearer settings)
        {
            this._provider = provider;
            this._settings = settings;
        }

        private async Task OnTokenValidated(TokenValidatedContext context)
        {
            using (var scope = this._provider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<QpancContext>();
                var loggedUser = scope.ServiceProvider.GetRequiredService<ILoggedUser>();
                var sessionId = Guid.Parse(context.Principal.Claims
                    .Where(x => x.Type == JwtRegisteredClaimNames.Jti)
                    .Select(x => x.Value)
                    .FirstOrDefault());

                var session = await db.Sessions.FindAsync(sessionId);
                if (session == null)
                {
                    context.Fail("");
                }
                else
                {
                    context.Success();
                }
            }
        }

        private async Task OnMessageReceived(MessageReceivedContext context)
        {
            await Task.Yield();
            // var urlPath = context.Request.Path.ToString();
            // if (urlPath.StartsWith("/signalr/") && context.Request.Query.ContainsKey("bearer"))
            //     context.Token = context.Request.Query["bearer"];
        }

        public void Configure(JwtBearerOptions options)
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = _settings.ValidIssuer,
                ValidAudience = _settings.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(_settings.IssuerSigningKey),
                TokenDecryptionKey = new SymmetricSecurityKey(_settings.TokenDecryptionKey),
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = OnMessageReceived,
                OnTokenValidated = OnTokenValidated
            };
        }
    }
}
