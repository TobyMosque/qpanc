using Microsoft.Extensions.Configuration;
using QPANC.Services.Abstract;
using IConfiguration = QPANC.Services.Abstract.IConfiguration;

namespace QPANC.Services
{
    public class ConnectionStrings : IConnectionStrings
    {
        private IConfiguration _configuration;

        public ConnectionStrings(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string DefaultConnection { get { return this._configuration.Root.GetValue<string>("DEFAULT_CONNECTION"); } }
        public string AuditConnection { get { return this._configuration.Root.GetValue<string>("AUDIT_CONNECTION"); } }
    }
}
