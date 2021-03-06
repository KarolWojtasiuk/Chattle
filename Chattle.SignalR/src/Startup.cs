using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Chattle.Database;

namespace Chattle.SignalR
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var mongo = new MongoDatabase(_configuration["MongoConnectionString"], _configuration["MongoDatabaseName"]);
            services.AddSingleton(new Chattle(mongo));

            services.AddAuthentication("BasicAuthentication")
            .AddScheme<AuthenticationSchemeOptions, ChattleAuthentication>("BasicAuthentication", null);
            services.AddAuthorization(o =>
            {
                o.DefaultPolicy = new AuthorizationPolicy(new List<ClaimsAuthorizationRequirement>
                {
                   new ClaimsAuthorizationRequirement(ClaimTypes.Authentication, new List<string>{"true"})
                },
                new List<string>
                {
                    "BasicAuthentication"
                });
            });

            services.AddSignalR();

            if (_configuration["CorsAllowedOrigins"] == "*")
            {
                services.AddCors(o => o.AddDefaultPolicy(p =>
                {
                    p.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(_ => true);
                }));
            }
            else
            {
                services.AddCors(o => o.AddDefaultPolicy(p =>
                {
                    var origins = _configuration.GetSection("CorsAllowedOrigins").Get<string[]>();
                    p.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
                }));
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors();
            app.UseRouting();

            app.UseQueryAuthentication();
            app.UseUserAuthentication();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChattleHub>("/chattle");
            });
        }
    }
}
