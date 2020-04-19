using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QPANC.Services.Abstract;
using System.Threading.Tasks;

namespace QPANC.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAuthentication _authentication;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthController(IAuthentication authentication, ITokenGenerator tokenGenerator, ILogger<WeatherForecastController> logger)
        {
            this._authentication = authentication;
            this._tokenGenerator = tokenGenerator;
            this._logger = logger;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var result = await this._authentication.Login(model);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var token = await this._tokenGenerator.Generate(result.Data);
                return Ok(token);
            }
            return this.ParseResult(result);
        }

        [Authorize]
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Logout()
        {
            var result = await this._authentication.Logout();
            return this.ParseResult(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            var result = await this._authentication.Register(model);
            return this.ParseResult(result);
        }
    }
}
