using System;
using System.Security.Claims;

namespace Chattle.SignalR
{
    public static class IdentityExtensions
    {
        public static Guid GetId(this ClaimsPrincipal identity)
        {
            Claim claim = identity?.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                return Guid.Empty;
            }

            return Guid.Parse(claim.Value);
        }
    }
}
