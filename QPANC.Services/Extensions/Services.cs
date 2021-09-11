using Askmethat.Aspnet.JsonLocalizer.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using QPANC.Services.Abstract.I18n;
using QPANC.Services.I18n;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace QPANC.Services.Extensions
{
    public static class Services
    {
        public static void AddI18n(this IServiceCollection services)
        {
            var defaultCulture = "pt";
            var supportedCultures = new[]
            {
                new CultureInfo("pt"),
                new CultureInfo("en")
            };

            services.AddJsonLocalization(options =>
            {
                options.ResourcesPath = "Resources";
                options.FileEncoding = Encoding.UTF8;
                options.LocalizationMode = Askmethat.Aspnet.JsonLocalizer.JsonOptions.LocalizationMode.I18n;
                options.SupportedCultureInfos = new HashSet<CultureInfo>(supportedCultures);
            });
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddScoped<IMessages, Messages>();
        }
    }
}
