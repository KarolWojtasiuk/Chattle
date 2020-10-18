using System;
using System.Linq;
using Chattle.Database;

namespace Chattle
{
    public class AccountController
    {
        private readonly IDatabase _database;
        private readonly ModelCleaner _modelCleaner;
        public string CollectionName { get; set; }

        public AccountController(IDatabase database, string collectionName, ModelCleaner modelCleaner)
        {
            _database = database;
            _modelCleaner = modelCleaner;
            CollectionName = collectionName;

            var count = _database.Count<Account>(collectionName, _ => true);
            if (count == 0)
            {
                var rootAccount = new Account("ROOT")
                {
                    Id = Chattle.SpecialId,
                    IsActive = true,
                    GlobalPermissions = AccountGlobalPermission.Administrator
                };
                rootAccount.ChangePassword("Chattle");

                _database.Create(collectionName, rootAccount);
            }
        }

        private void VerifyUsername(string username, Guid id)
        {
            if (username.Length < 5)
            {
                throw new ModelVerificationException<Account>(id, "Username should be at least 5 characters long.");
            }
            else if (String.IsNullOrWhiteSpace(username))
            {
                throw new ModelVerificationException<Account>(id, "Username should not be empty or contain only whitespace.");
            }
        }

        public void Create(Account account)
        {
            VerifyUsername(account.Username, account.Id);
            PermissionHelper.CreateAccount(account, _database, CollectionName);
            _database.Create(CollectionName, account);
        }

        public Account Get(Guid id, Guid callerId)
        {
            PermissionHelper.GetAccount(id, callerId, _database, CollectionName);
            return _database.Read<Account>(CollectionName, a => a.Id == id, 1).FirstOrDefault();
        }

        public void Delete(Guid id, Guid callerId)
        {
            PermissionHelper.DeleteAccount(id, callerId, _database, CollectionName);
            _database.Delete<Account>(CollectionName, id);
            _modelCleaner.CleanFromAccount(id);
        }

        public void SetActive(Guid id, bool isActive, Guid callerId)
        {
            PermissionHelper.ManageAccount(id, callerId, _database, CollectionName);
            _database.Update<Account>(CollectionName, id, "IsActive", isActive);
        }

        public void SetGlobalPermission(Guid id, AccountGlobalPermission permission, Guid callerId)
        {
            PermissionHelper.ManageAccount(id, callerId, _database, CollectionName);
            _database.Update<Account>(CollectionName, id, "GlobalPermissions", permission);
        }

        public void SetUsername(Guid id, string username, Guid callerId)
        {
            VerifyUsername(username, id);
            PermissionHelper.ModifyAccount(id, callerId, _database, CollectionName);
            _database.Update<Account>(CollectionName, id, "Username", username);
        }

        public void SetPassword(Guid id, string password, Guid callerId)
        {
            PermissionHelper.ModifyAccount(id, callerId, _database, CollectionName);
            var account = _database.Read<Account>(CollectionName, a => a.Id == id, 1).FirstOrDefault();
            account.ChangePassword(password);
            _database.Replace(CollectionName, account.Id, account);
        }
    }
}
