using Microsoft.Extensions.Configuration;

namespace QPANC.Services.Abstract
{
    public interface IConfiguration
    {
        IConfigurationRoot Root { get; }
    }
}
