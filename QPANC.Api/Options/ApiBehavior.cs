using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using QPANC.Services.Abstract.I18n;
using System.Net;

namespace QPANC.Api.Options
{
    public class ApiBehavior : IConfigureOptions<ApiBehaviorOptions>
    {
        public ApiBehavior()
        {
        }

        public void Configure(ApiBehaviorOptions options)
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var provider = context.HttpContext.RequestServices;
                var messages = provider.GetRequiredService<IMessages>();
                var details = new ValidationProblemDetails(context.ModelState)
                {
                    Title = messages.Text_ProblemDetails,
                    Status = (int)HttpStatusCode.UnprocessableEntity
                };
                return new UnprocessableEntityObjectResult(details);
            };
        }
    }
}
