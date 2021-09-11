using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using IConfiguration = QPANC.Services.Abstract.IConfiguration;

namespace QPANC.Services
{
    public class Configuration : IConfiguration
    {
        public IConfigurationRoot Root { get; }

        public Configuration(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Root = builder.Build();
        }
    }
}
