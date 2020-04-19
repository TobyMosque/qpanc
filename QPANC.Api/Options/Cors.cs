using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;
using QPANC.Services.Abstract;
using System;
using System.Linq;

namespace QPANC.Api.Options
{
    public class Cors : IConfigureOptions<CorsOptions>
    {
        private ICors _settings;

        public Cors(ICors settings)
        {
            this._settings = settings;
        }

        public void Configure(CorsOptions options)
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .SetIsOriginAllowed(url => {
                        var host = new Uri(url).Host;
                        return this._settings.Hosts.Contains(host); ;
                    })
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        }
    }
}
