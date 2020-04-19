using System.Threading.Tasks;

namespace QPANC.Services.Abstract
{
    public interface IAuthentication
    {
        Task<BaseResponse<LoginResponse>> Login(LoginRequest login);
        Task<BaseResponse> Logout();
        Task<BaseResponse> Register(RegisterRequest register);
    }
}
