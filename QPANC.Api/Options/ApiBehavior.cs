using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using QPANC.Services.Abstract;
using System.Net;

namespace QPANC.Api.Options
{
    public class ApiBehavior : IConfigureOptions<ApiBehaviorOptions>
    {
        private readonly IStringLocalizer _localizer;

        public ApiBehavior(IStringLocalizer<Messages> localizer)
        {
            this._localizer = localizer;
        }

        public void Configure(ApiBehaviorOptions options)
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var details = new ValidationProblemDetails(context.ModelState)
                {
                    Title = this._localizer[nameof(Messages.Text_ProblemDetails)],
                    Status = (int)HttpStatusCode.UnprocessableEntity
                };
                return new UnprocessableEntityObjectResult(details);
            };
        }
    }
}
