using Chattle.Models;

namespace Chattle
{
    public class PermissionChecker
    {
        public bool CanGetAccount(Account requesterAccount)
        {
            // Account can get accounts if it is active;
            return requesterAccount.IsActive;
        }

        public bool CanGetUser(Account requesterAccount)
        {
            // Account can get users if it is active;
            return requesterAccount.IsActive;
        }
    }
}