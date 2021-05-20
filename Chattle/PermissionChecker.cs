using System;
using Chattle.Database;

namespace Chattle
{
    public class PermissionChecker
    {
        public PermissionChecker(DatabaseController databaseController)
        {
            _databaseController = databaseController;
        }

        private readonly DatabaseController _databaseController;

        public bool CanGetAccount(Guid requesterAccountId)
        {
            var requester = _databaseController.Accounts.Get(requesterAccountId);

            return requester?.IsActive ?? false;
        }
    }
}