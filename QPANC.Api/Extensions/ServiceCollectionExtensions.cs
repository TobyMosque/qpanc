using Microsoft.Extensions.DependencyInjection;
using QPANC.Services;
using QPANC.Services.Abstract;

namespace QPANC.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppSettings(this IServiceCollection services)
        {
            services.AddSingleton<IConfiguration, Configuration>();
            services.AddSingleton<IConnectionStrings, ConnectionStrings>();
            services.AddSingleton<IJwtBearer, JwtBearer>();
            services.AddSingleton<ICors, Cors>();
            services.AddSingleton<IAppSettings, AppSettings>();
        }
    }
}
