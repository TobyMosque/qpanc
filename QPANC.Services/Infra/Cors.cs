using Microsoft.Extensions.Configuration;
using QPANC.Services.Abstract;
using System.Linq;
using IConfiguration = QPANC.Services.Abstract.IConfiguration;

namespace QPANC.Services
{
    public class Cors : ICors
    {
        private IConfiguration _configuration;

        public Cors(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string[] Hosts
        { 
            get 
            {
                var hosts = this._configuration.Root
                    .GetSection("CORS_HOSTS")
                    .AsEnumerable()
                    .Select(pair => pair.Value)
                    .Where(value => !string.IsNullOrWhiteSpace(value))
                    .ToArray();
                return hosts;
            }
        }
    }
}
