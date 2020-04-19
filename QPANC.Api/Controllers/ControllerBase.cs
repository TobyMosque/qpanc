using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using QPANC.Services.Abstract;

namespace QPANC.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        internal IActionResult ParseResult<T>(BaseResponse<T> result)
        {
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result.Data);
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();
                return options.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            else
            {
                return StatusCode((int)result.StatusCode);
            }
        }

        internal IActionResult ParseResult(BaseResponse result)
        {
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok();
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();
                return options.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            else
            {
                return StatusCode((int)result.StatusCode);
            }
        }
    }
}
