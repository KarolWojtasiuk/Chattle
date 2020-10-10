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

        private void VerifyUsername(string username, Guid accountId)
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
            PermissionHelper.CreateAccount();
            _database.Create(_collectionName, account);
        }

        public Account Get(Guid id, Guid callerId)
        {
            PermissionHelper.GetAccount(callerId, _database, _collectionName);
            return _database.Read<Account>(_collectionName, a => a.Id == id, 1).FirstOrDefault();
        }

        public void Delete(Guid id, Guid callerId)
        {
            PermissionHelper.DeleteAccount(id, callerId, _database, _collectionName);
            _database.Delete<Account>(_collectionName, id);
        }

        public void SetActive(Guid id, bool isActive, Guid callerId)
        {
            PermissionHelper.ManageAccount(id, callerId, _database, _collectionName);
            _database.Update<Account>(_collectionName, id, "IsActive", isActive);
        }

        public void SetUsername(Guid id, string username, Guid callerId)
        {
            VerifyUsername(username, id);
            PermissionHelper.ModifyAccount(id, callerId, _database, _collectionName);
            _database.Update<Account>(_collectionName, id, "Username", username);
        }

        public void SetPassword(Guid id, string password, Guid callerId)
        {
            PermissionHelper.ModifyAccount(id, callerId, _database, _collectionName);
            var account = _database.Read<Account>(_collectionName, a => a.Id == id, 1).FirstOrDefault();
            account.ChangePassword(password);
            _database.Replace(_collectionName, account.Id, account);
        }
    }
}
