using Microsoft.Extensions.Configuration;
using QPANC.Services.Abstract;
using System;
using IConfiguration = QPANC.Services.Abstract.IConfiguration;

namespace QPANC.Services
{
    public class JwtBearer : IJwtBearer
    {
        private IConfiguration _configuration;

        public JwtBearer(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string ValidIssuer { get { return this._configuration.Root.GetValue<string>("JWTBEARER_VALIDISSUER"); } }

        public string ValidAudience { get { return this._configuration.Root.GetValue<string>("JWTBEARER_VALIDAUDIENCE"); } }

        public byte[] IssuerSigningKey { get { return this.Base64AsBinary("JWTBEARER_ISSUERSIGNINGKEY"); } }

        public byte[] TokenDecryptionKey { get { return this.Base64AsBinary("JWTBEARER_TOKENDECRYPTIONKEY"); } }

        private byte[] Base64AsBinary(string key)
        {
            var base64 = _configuration.Root.GetValue<string>(key);
            if (string.IsNullOrWhiteSpace(base64))
                return new byte[] { };
            return Convert.FromBase64String(base64);
        }
    }
}
