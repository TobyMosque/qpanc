using System.Threading.Tasks;

namespace QPANC.Services.Abstract
{
    public interface ITokenGenerator
    {
        Task<string> Generate(LoginResponse userId);
    }
}
