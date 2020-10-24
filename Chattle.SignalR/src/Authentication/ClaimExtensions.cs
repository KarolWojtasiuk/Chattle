using System;
using System.Security.Claims;

namespace Chattle.SignalR
{
    public static class IdentityExtensions
    {
        public static Guid GetAccountId(this ClaimsPrincipal identity)
        {
            Claim claim = identity?.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                return Guid.Empty;
            }

            return Guid.Parse(claim.Value);
        }

        public static Guid GetUserId(this ClaimsPrincipal identity)
        {
            Claim claim = identity?.FindFirst(ClaimTypes.UserData);

            if (claim == null)
            {
                throw new ArgumentNullException("UserId");
            }

            return Guid.Parse(claim.Value);
        }
    }
}
