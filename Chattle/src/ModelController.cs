using System;
using System.Collections.Generic;
using Chattle.Database;

namespace Chattle
{
    public class ModelController
    {
        public List<IDatabase> Databases;

        public ModelController()
        {
            Databases = new List<IDatabase>();
        }

        #region Account
        public void CreateAccount(Account account)
        {
            foreach (var database in Databases)
            {
                database.Create("Accounts", account);
            }
        }

        public void DeleteAccount(Account account)
        {
            foreach (var database in Databases)
            {
                database.Delete<Account>("Accounts", account.Id);
            }
        }

        public void FindAccount(Guid accountId)
        {
            foreach (var database in Databases)
            {
                database.Read<Account>("Accounts", a => a.Id == accountId);
            }
        }

        public void ActivateAccount(Account account)
        {
            account.IsActive = true;
            foreach (var database in Databases)
            {
                database.Update("Accounts", account.Id, account);
            }
        }

        public void DeactivateAccount(Account account)
        {
            account.IsActive = false;
            foreach (var database in Databases)
            {
                database.Update("Accounts", account.Id, account);
            }
        }

        public void ChangeAccountUsername(Account account, string newUsername)
        {
            account.Username = newUsername;
            foreach (var database in Databases)
            {
                database.Update("Accounts", account.Id, account);
            }
        }

        public void ChangeAccountPassword(Account account, string newPassword)
        {
            account.ChangePassword(newPassword);
            foreach (var database in Databases)
            {
                database.Update("Accounts", account.Id, account);
            }
        }
        #endregion

        #region User
        #endregion

        #region Server
        #endregion

        #region Channel
        #endregion

        #region Message
        #endregion

        #region Role
        #endregion
    }
}
