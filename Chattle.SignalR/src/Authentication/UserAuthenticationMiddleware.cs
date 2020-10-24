using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace Chattle.SignalR
{
    public class UserAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public UserAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers["Connection"] == "Upgrade" && context.Request.Query.TryGetValue("userId", out var token))
            {
                context.Request.Headers.Add("UserId", token.First());
            }
            await _next.Invoke(context);
        }
    }

    public static class UserAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserAuthenticationMiddleware>();
        }
    }
}
