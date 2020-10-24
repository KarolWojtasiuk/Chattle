using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;

namespace Chattle.SignalR
{
    public class ChattleAuthentication : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly Chattle _chattle;

        public ChattleAuthentication(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, Chattle chattle) : base(options, logger, encoder, clock)
        {
            _chattle = chattle;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Context.GetEndpoint()?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return AuthenticateResult.NoResult();
            }

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            Account account = null;

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var authBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(authBytes).Split(':', 2);
                var id = Guid.Parse(credentials[0]);
                var password = credentials[1];

                if (Guid.Parse(id.ToString()) == Guid.Empty)
                {
                    throw new Exception();
                }

                account = await Task.Run(() => { return _chattle.AccountController.Get(id, id); });

                if (!account.VerifyPassword(password))
                {
                    throw new Exception();
                }
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            var userId = Guid.Empty;

            if (!String.IsNullOrWhiteSpace(Request.Headers["UserId"][0]))
            {
                try
                {
                    var userHeader = Request.Headers["UserId"][0];
                    var userBytes = Convert.FromBase64String(userHeader);
                    var userIdString = Encoding.UTF8.GetString(userBytes);
                    userId = new Guid(userIdString);
                }
                catch
                {
                    return AuthenticateResult.Fail("Invalid UserId Header");
                }
            }


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Authentication, "true"),
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString())
            };

            if (userId != Guid.Empty)
            {
                claims.Add(new Claim(ClaimTypes.UserData, userId.ToString()));
            }

            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
