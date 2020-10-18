using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace Chattle.SignalR
{
    public class QueryAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public QueryAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers["Connection"] == "Upgrade" && context.Request.Query.TryGetValue("authorization", out var token))
            {
                context.Request.Headers.Add("Authorization", "Basic " + token.First());
            }
            await _next.Invoke(context);
        }
    }

    public static class QueryAuthenticationExtensions
    {
        public static IApplicationBuilder UseQueryAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<QueryAuthenticationMiddleware>();
        }
    }
}
