using QPANC.Services.Abstract;

namespace QPANC.Services
{
    public class AppSettings : IAppSettings
    {
        public IConnectionStrings ConnectionString { get; }
        public IJwtBearer JwtBearer { get; }
        public ICors Cors { get; }

        public AppSettings(IConnectionStrings connectionStrings, IJwtBearer jwtBetter, ICors cors)
        {
            this.ConnectionString = connectionStrings;
            this.JwtBearer = jwtBetter;
            this.Cors = cors;
        }
    }
}
