using System;
using System.Linq;
using Chattle.Database;

namespace Chattle
{
    public class AccountController
    {
        private readonly IDatabase _database;
        private readonly string _collectionName;

        public AccountController(IDatabase database, string collectionName)
        {
            _database = database;
            _collectionName = collectionName;
        }

        private bool HasGlobalPermission(Guid id, AccountGlobalPermission permission)
        {
            return _database.Read<Account>(_collectionName, a => a.Id == id, 1).FirstOrDefault().GlobalPermissions.HasFlag(permission);
        }

        private static void VerifyUsername(string username, Guid accountId)
        {
            if (username.Length < 5)
            {
                throw new ModelVerificationException<Account>(accountId, "Username should be at least 5 characters long.");
            }
            else if (String.IsNullOrWhiteSpace(username))
            {
                throw new ModelVerificationException<Account>(accountId, "Username should not be empty or contain only whitespace.");
            }
        }

        public void Create(Account account)
        {
            VerifyUsername(account.Username, account.Id);
            _database.Create(_collectionName, account);
        }

        public Account Read(Guid id, Guid callerId)
        {
            if (_database.Count<Account>(_collectionName, a => a.Id == callerId && a.IsActive == true) <= 0)
            {
                throw new InsufficientPermissionsException(callerId);
            }

            return _database.Read<Account>(_collectionName, a => a.Id == id, 1).FirstOrDefault();
        }

        public void Delete(Guid id, Guid callerId)
        {
            if (id != callerId || !HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts))
            {
                throw new InsufficientPermissionsException(callerId);
            }

            _database.Delete<Account>(_collectionName, id);
        }

        public void SetActive(Guid id, bool isActive, Guid callerId)
        {
            if (!HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts))
            {
                throw new InsufficientPermissionsException(callerId);
            }

            _database.Update<Account>(_collectionName, id, "IsActive", isActive);
        }

        public void SetUsername(Guid id, string username, Guid callerId)
        {
            VerifyUsername(username, id);
            if (id != callerId || !HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts))
            {
                throw new InsufficientPermissionsException(callerId);
            }

            _database.Update<Account>(_collectionName, id, "Username", username);
        }
    }
}
