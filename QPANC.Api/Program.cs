using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace QPANC.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // var random = RandomNumberGenerator.Create();
            // var binarySigningKey = new byte[64];
            // var binaryTokenDecryptionKey = new byte[32];
            // random.GetBytes(binarySigningKey);
            // random.GetBytes(binaryTokenDecryptionKey);
            // var signingKey = Convert.ToBase64String(binarySigningKey);
            // var tokenDecryptionKey = Convert.ToBase64String(binaryTokenDecryptionKey);
            // Console.WriteLine($"Sign: ${signingKey} | Decrypt: ${tokenDecryptionKey}");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
