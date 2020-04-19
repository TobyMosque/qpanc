using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using IConfiguration = QPANC.Services.Abstract.IConfiguration;

namespace QPANC.Services
{
    public class Configuration : IConfiguration
    {
        public IConfigurationRoot Root { get; }

        public Configuration(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Root = builder.Build();
        }
    }
}
