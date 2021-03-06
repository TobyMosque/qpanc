using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using QPANC.Api.Extensions;
using QPANC.Api.Middlewares;
using QPANC.Api.Services;
using QPANC.Domain;
using QPANC.Domain.Identity;
using QPANC.Services;
using QPANC.Services.Abstract;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace QPANC.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private IServiceProvider _provider;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("pt"),
                    new CultureInfo("en")
                };

                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddControllers()
                .AddDataAnnotationsLocalization(options => 
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        return factory.Create(typeof(Messages));
                    };
                });
            services.ConfigureOptions<Options.ApiBehavior>();

            services.AddAppSettings();
            services.AddScoped<ILoggedUser, LoggedUser>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IAuthentication, Authentication>();
            services.AddDbContext<QpancContext>();
            services.AddIdentity<User, Role>()
               .AddRoles<Role>()
               .AddEntityFrameworkStores<QpancContext>()
               .AddDefaultTokenProviders();
            services.AddTriggers();
            services.AddSingleton<ISGuid, SGuid>();
            services.AddScoped<ISeeder, Seeder>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
                authenticationScheme: JwtBearerDefaults.AuthenticationScheme,
                configureOptions: options =>
                {
                    _provider.GetRequiredService<IConfigureOptions<JwtBearerOptions>>().Configure(options);
                });
            services.ConfigureOptions<Options.JwtBearer>();
            services.ConfigureOptions<Options.Cors>();
            services.AddCors();

            services.AddSwaggerGen(config =>
            {
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                var requirements = new OpenApiSecurityRequirement();
                var bearerSchema = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                };
                requirements.Add(bearerSchema, new string[] { });
                config.AddSecurityRequirement(requirements);
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API - QPANC - Quasar, PostgreSQL, ASP.NET Core and Docker",
                    Description = "API - QPANC - Quasar, PostgreSQL, ASP.NET Core and Docker",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "QPANC - Quasar, PostgreSQL, ASP.NET Core and Docker",
                        Email = "developer@qpanc.app",
                        Url = new Uri("http://www.tudosobreplantas.com")
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeeder seeder)
        {
            this._provider = app.ApplicationServices;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRequestLocalization();

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (!env.IsDevelopment())
            {
                app.UseMiddleware<SwaggerBasicAuthMiddleware>();
            }
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "QPANC - Quasar, PostgreSQL, ASP.NET Core and Docker");
                c.RoutePrefix = "swagger";
            });

            seeder.Execute().GetAwaiter().GetResult();
        }
    }
}
