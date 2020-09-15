using System;
using System.Linq;
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
            if (Databases.FirstOrDefault().Count<Account>("Accounts", a => a.Id == account.Id) > 0)
            {
                ModelVerifier.ThrowDuplicateException(account);
            }
            ModelVerifier.VerifyAccount(account);

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

        public List<Account> FindAccount(Guid accountId)
        {
            return Databases.FirstOrDefault().Read<Account>("Accounts", a => a.Id == accountId);
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
            ModelVerifier.VerifyAccountUsername(account);

            foreach (var database in Databases)
            {
                database.Update("Accounts", account.Id, account);
            }
        }

        public void ChangeAccountPassword(Account account, string newPassword)
        {
            account.ChangePassword(newPassword);
            ModelVerifier.VerifyAccountPassword(account, newPassword);

            foreach (var database in Databases)
            {
                database.Update("Accounts", account.Id, account);
            }
        }
        #endregion

        #region User
        public void CreateUser(User user, Account ownerAccount)
        {
            if (Databases.FirstOrDefault().Count<User>("Users", u => u.Id == user.Id) > 0)
            {
                ModelVerifier.ThrowDuplicateException(user);
            }

            if (Databases.FirstOrDefault().Read<Account>("Accounts", a => a.Id == ownerAccount.Id).Count == 0)
            {
                ModelVerifier.ThrowDoesNotExistsException();
            }

            if (user.Type == UserType.User)
            {
                var usersCount = Databases.FirstOrDefault().Count<User>("Users", u => u.AccountId == ownerAccount.Id && u.Type == UserType.User);

                if (usersCount > 0)
                {
                    ModelVerifier.AnotherUserAssignedException(ownerAccount);
                }
            }
            ModelVerifier.VerifyUser(user);

            foreach (var database in Databases)
            {
                database.Create("Users", user);
            }
        }

        public void DeleteUser(User user)
        {
            foreach (var database in Databases)
            {
                database.Delete<Account>("Users", user.Id);
            }
        }

        public List<User> FindUser(Guid userId)
        {
            return Databases.FirstOrDefault().Read<User>("Users", a => a.Id == userId);
        }

        public void ActivateUser(User user)
        {
            user.IsActive = true;

            foreach (var database in Databases)
            {
                database.Update("Users", user.Id, user);
            }
        }

        public void DeactivateUser(User user)
        {
            user.IsActive = false;

            foreach (var database in Databases)
            {
                database.Update("Users", user.Id, user);
            }
        }

        public void ChangeUserNickname(User user, string newNickname)
        {
            user.Nickname = newNickname;
            ModelVerifier.VerifyUserNickname(user);

            foreach (var database in Databases)
            {
                database.Update("Users", user.Id, user);
            }
        }

        public void ChangeUserImage(User user, Uri newImage)
        {
            user.Image = newImage;
            ModelVerifier.VerifyUserImage(user);

            foreach (var database in Databases)
            {
                database.Update("Users", user.Id, user);
            }
        }

        public void RestoreDefaultUserImage(User user)
        {
            user.Image = user.GetDefaultUserImage();
            ModelVerifier.VerifyUserImage(user);

            foreach (var database in Databases)
            {
                database.Update("Users", user.Id, user);
            }
        }

        #endregion

        #region Server
        public void CreateServer(Server server)
        {
            if (Databases.FirstOrDefault().Count<Server>("Servers", s => s.Id == server.Id) > 0)
            {
                ModelVerifier.ThrowDuplicateException(server);
            }
            ModelVerifier.VerifyServer(server, Databases.FirstOrDefault().Read<User>("Users", u => u.Id == server.OwnerId).FirstOrDefault());

            foreach (var database in Databases)
            {
                database.Create("Servers", server);
            }
        }

        public void DeleteServer(Server server, Guid callerId)
        {
            if (server.OwnerId != callerId)
            {
                ModelVerifier.ThrowNotOwnerException(server);
            }

            foreach (var database in Databases)
            {
                database.Delete<Server>("Servers", server.Id);
            }
        }

        public List<Server> FindServer(Guid serverId)
        {
            return Databases.FirstOrDefault().Read<Server>("Servers", s => s.Id == serverId);
        }

        public void ChangeServerName(Server server, string newName, Guid callerId)
        {
            ModelVerifier.CheckPermission(callerId, server.Roles, Permission.ManageServer);

            server.Name = newName;
            ModelVerifier.VerifyServerName(server);

            foreach (var database in Databases)
            {
                database.Update("Servers", server.Id, server);
            }
        }

        public void ChangeServerDescription(Server server, string newDescription, Guid callerId)
        {
            ModelVerifier.CheckPermission(callerId, server.Roles, Permission.ManageServer);

            server.Description = newDescription;

            foreach (var database in Databases)
            {
                database.Update("Servers", server.Id, server);
            }
        }

        public void ChangeServerImage(Server server, Uri newImage, Guid callerId)
        {
            ModelVerifier.CheckPermission(callerId, server.Roles, Permission.ManageServer);

            server.Image = newImage;
            ModelVerifier.VerifyServerImage(server);

            foreach (var database in Databases)
            {
                database.Update("Servers", server.Id, server);
            }
        }

        public void ChangeServerRoles(Server server, Guid callerId)
        {
            ModelVerifier.CheckPermission(callerId, server.Roles, Permission.ManageServer);

            ModelVerifier.VerifyServerRoles(server);

            foreach (var database in Databases)
            {
                database.Update("Servers", server.Id, server);
            }
        }
        #endregion

        #region Channel
        #endregion

        #region Message
        #endregion

        #region Role
        #endregion
    }
}
