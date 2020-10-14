using System;
using System.Linq;
using System.Collections.Generic;
using Chattle.Database;

namespace Chattle
{
    public static class PermissionHelper
    {
        #region Account
        public static void CreateAccount(Account account, IDatabase database, string collectionName)
        {
            //? Anyone can create an account;

            if (Exists<Account>(account.Id, database, collectionName))
            {
                throw new AlreadyExistsException<Account>(account.Id);
            }
        }

        public static void GetAccount(Guid id, Guid callerId, IDatabase database, string collectionName)
        {
            //? Active account can get accounts;

            if (!Exists<Account>(id, database, collectionName))
            {
                throw new DoesNotExistException<Account>(id);
            }

            if (!AccountIsActive(callerId, database, collectionName))
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }

        public static void DeleteAccount(Guid id, Guid callerId, IDatabase database, string collectionName)
        {
            //? 1. Account can delete own account;
            //? 2. Active account with global permission `ManageAccounts` can delete other accounts;

            if (!Exists<Account>(id, database, collectionName))
            {
                throw new DoesNotExistException<Account>(id);
            }

            var firstCondition = id == callerId;
            var secondCondition = HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts, database, collectionName) && AccountIsActive(callerId, database, collectionName);

            if (!(firstCondition && secondCondition))
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }

        public static void ModifyAccount(Guid id, Guid callerId, IDatabase database, string collectionName)
        {
            //? 1. Active account can modify own account;
            //? 2. Active account with global permission `ManageAccounts` can modify other accounts;

            if (!Exists<Account>(id, database, collectionName))
            {
                throw new DoesNotExistException<Account>(id);
            }

            var firstCondition = id == callerId;
            var secondCondition = HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts, database, collectionName);

            if (!((firstCondition || secondCondition) && AccountIsActive(callerId, database, collectionName)))
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }

        public static void ManageAccount(Guid id, Guid callerId, IDatabase database, string collectionName)
        {
            //? Active account with global permission `ManageAccounts` can delete account;

            if (!Exists<Account>(id, database, collectionName))
            {
                throw new DoesNotExistException<Account>(id);
            }

            if (!(HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts, database, collectionName) && AccountIsActive(callerId, database, collectionName)))
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }
        #endregion

        #region User
        public static void CreateUser(User user, Guid callerId, IDatabase database, string collectionName, AccountController accountController)
        {
            //? 1. Active account can create user for own account;
            //? 2. Active account with global permission `ManageAccounts` can create users for other accounts;
            //? Limited to one `UserType.User` per account;

            if (Exists<User>(user.Id, database, collectionName))
            {
                throw new AlreadyExistsException<User>(user.Id);
            }

            var firstCondition = user.AccountId == callerId;
            var secondCondition = HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts, database, accountController.CollectionName);

            if (user.Type == UserType.User)
            {
                if (database.Count<User>(collectionName, u => u.AccountId == user.AccountId) > 0)
                {
                    throw new ModelVerificationException<Account>(user.AccountId, "Only one user can be assigned to an account.");
                }
            }

            if (!((firstCondition || secondCondition) && AccountIsActive(callerId, database, collectionName)))
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }

        public static void GetUser(Guid id, Guid callerId, IDatabase database, string collectionName, AccountController accountController)
        {
            //? Active account can get users;

            if (!Exists<User>(id, database, collectionName))
            {
                throw new DoesNotExistException<User>(id);
            }

            if (!AccountIsActive(callerId, database, accountController.CollectionName))
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }

        public static void DeleteUser(Guid id, Guid callerId, IDatabase database, string collectionName, AccountController accountController)
        {
            //? 1. Active account can delete own users;
            //? 2. Active account with global permission `ManageAccounts` can delete other accounts;

            if (!Exists<User>(id, database, collectionName))
            {
                throw new DoesNotExistException<User>(id);
            }

            var firstCondition = id == callerId;
            var secondCondition = HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts, database, accountController.CollectionName);

            if (!((firstCondition || secondCondition) && AccountIsActive(callerId, database, collectionName)))
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }

        public static void ModifyUser(Guid id, Guid callerId, IDatabase database, string collectionName, AccountController accountController)
        {
            //? 1. Active account can modify own users;
            //? 2. Active account with global permission `ManageAccounts` can modify other users;

            if (!Exists<User>(id, database, collectionName))
            {
                throw new DoesNotExistException<User>(id);
            }

            var firstCondition = id == callerId;
            var secondCondition = HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts, database, accountController.CollectionName);

            if (!((firstCondition || secondCondition) && AccountIsActive(callerId, database, collectionName)))
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }

        public static void ManageUser(Guid id, Guid callerId, IDatabase database, string collectionName, AccountController accountController)
        {
            //? Active account with global permission `ManageAccounts` can manage other users;

            if (!Exists<User>(id, database, collectionName))
            {
                throw new DoesNotExistException<User>(id);
            }

            if (!(HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts, database, accountController.CollectionName) && AccountIsActive(callerId, database, collectionName)))
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }
        #endregion

        #region Server
        public static void CreateServer(Server server, Guid callerId, IDatabase database, string collectionName, UserController userController, AccountController accountController)
        {
            //? 1. Active user with active account can create own servers;
            //? 2. Active user with active account and global permission `ManageServers` can create server for other users;

            if (Exists<Server>(server.Id, database, collectionName))
            {
                throw new AlreadyExistsException<Server>(server.Id);
            }

            var caller = database.Read<User>(userController.CollectionName, u => u.Id == callerId, 1).FirstOrDefault();

            var firstCondition = server.OwnerId == callerId;
            var secondCondition = HasGlobalPermission(caller, UserGlobalPermission.ManageServers);

            if (!(firstCondition || secondCondition))
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }

            if (!caller.IsActive)
            {
                throw new InsufficientPermissionsException<User>(callerId);
            }

            if (!AccountIsActive(caller.AccountId, database, accountController.CollectionName))
            {
                throw new InsufficientPermissionsException<Account>(caller.AccountId);
            }
        }

        public static void GetServer(Guid id, Guid callerId, IDatabase database, string collectionName, UserController userController, AccountController accountController)
        {
            //? Active user with active account can get servers;

            if (!Exists<Server>(id, database, collectionName))
            {
                throw new DoesNotExistException<Server>(id);
            }

            var caller = database.Read<User>(userController.CollectionName, u => u.Id == callerId, 1).FirstOrDefault();

            if (!caller.IsActive)
            {
                throw new InsufficientPermissionsException<User>(callerId);
            }

            if (!AccountIsActive(caller.AccountId, database, accountController.CollectionName))
            {
                throw new InsufficientPermissionsException<Account>(caller.AccountId);
            }
        }

        public static void DeleteServer(Guid id, Guid callerId, IDatabase database, string collectionName, UserController userController, AccountController accountController, ServerController serverController)
        {
            //? 1. Active user with active account can delete own servers;
            //? 2. Active user with active account and global permission `ManageServers` can delete other servers;

            if (!Exists<Server>(id, database, collectionName))
            {
                throw new DoesNotExistException<Server>(id);
            }

            var server = database.Read<Server>(serverController.CollectionName, s => s.Id == id, 1).FirstOrDefault();
            var caller = database.Read<User>(userController.CollectionName, u => u.Id == callerId, 1).FirstOrDefault();

            var firstCondition = server.OwnerId == callerId;
            var secondCondition = HasGlobalPermission(caller, UserGlobalPermission.ManageServers);

            if (!(firstCondition || secondCondition))
            {
                throw new InsufficientPermissionsException<User>(callerId);
            }

            if (!UserIsActive(callerId, database, userController.CollectionName))
            {
                throw new InsufficientPermissionsException<User>(callerId);
            }

            if (!AccountIsActive(caller.AccountId, database, accountController.CollectionName))
            {
                throw new InsufficientPermissionsException<Account>(caller.AccountId);
            }
        }

        public static void ModifyServer(Guid id, Guid callerId, IDatabase database, string collectionName, UserController userController, AccountController accountController, ServerController serverController)
        {
            //? 1. Active user with active account can modify own servers;
            //? 2. Active user with active account and permission `ManageServer` can modify server;
            //? 3. Active user with active account and global permission `ManageServers` can modify other servers;

            if (!Exists<Server>(id, database, collectionName))
            {
                throw new DoesNotExistException<Server>(id);
            }

            var server = database.Read<Server>(serverController.CollectionName, s => s.Id == id, 1).FirstOrDefault();
            var caller = database.Read<User>(userController.CollectionName, u => u.Id == callerId, 1).FirstOrDefault();

            var firstCondition = server.OwnerId == callerId;
            var secondCondition = HasPermission(callerId, server.Roles, Permission.ManageServer);
            var thirdCondition = HasGlobalPermission(caller, UserGlobalPermission.ManageServers);

            if (!(firstCondition || secondCondition || thirdCondition))
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }

            if (!UserIsActive(callerId, database, userController.CollectionName))
            {
                throw new InsufficientPermissionsException<User>(callerId);
            }

            if (!AccountIsActive(caller.AccountId, database, accountController.CollectionName))
            {
                throw new InsufficientPermissionsException<Account>(caller.AccountId);
            }
        }
        #endregion

        #region Channel
        public static void CreateChannel(Channel channel, Guid callerId, IDatabase database, string collectionName, UserController userController, AccountController accountController, ServerController serverController)
        {
            //? 1. Active user with active account can create channels on own server;
            //? 2. Active user with active account and permission `ManageChannels` can create channels on server;
            //? 3. Active user with active account and global permission `ManageServers` can create channels as other user;

            if (!Exists<Server>(channel.ServerId, database, collectionName))
            {
                throw new DoesNotExistException<Server>(channel.ServerId);
            }

            var server = database.Read<Server>(serverController.CollectionName, s => s.Id == channel.ServerId, 1).FirstOrDefault();
            var caller = database.Read<User>(userController.CollectionName, u => u.Id == callerId, 1).FirstOrDefault();

            var firstCondition = channel.ServerId == server.Id && server.OwnerId == callerId;
            var secondCondition = channel.ServerId == server.Id && channel.AuthorId == callerId && HasPermission(callerId, server.Roles, Permission.ManageServer);
            var thirdCondition = HasGlobalPermission(caller, UserGlobalPermission.ManageServers);

            if (!(firstCondition || secondCondition || thirdCondition))
            {
                throw new InsufficientPermissionsException<User>(callerId);
            }

            if (!UserIsActive(callerId, database, userController.CollectionName))
            {
                throw new InsufficientPermissionsException<User>(callerId);
            }

            if (!AccountIsActive(caller.AccountId, database, accountController.CollectionName))
            {
                throw new InsufficientPermissionsException<Account>(caller.AccountId);
            }
        }

        public static void GetChannel(Guid id, Guid callerId, IDatabase database, string collectionName, UserController userController, AccountController accountController, ServerController serverController)
        {
            //? 1. Active user with active account with assigned basic role can get channels;
            //? 2. Active user with active account and global permission `ManageServers` can get channels;

            if (!Exists<Channel>(id, database, collectionName))
            {
                throw new DoesNotExistException<Server>(id);
            }

            var channel = database.Read<Channel>(collectionName, c => c.Id == id).FirstOrDefault();
            var server = database.Read<Server>(serverController.CollectionName, s => s.Id == channel.ServerId, 1).FirstOrDefault();
            var caller = database.Read<User>(userController.CollectionName, u => u.Id == callerId, 1).FirstOrDefault();

            var firstCondition = server.Roles.FirstOrDefault(r => r.Id == Guid.Empty).Users.Contains(callerId);
            var secondCondition = HasGlobalPermission(caller, UserGlobalPermission.ManageServers);

            if (!(firstCondition || secondCondition))
            {
                throw new InsufficientPermissionsException<User>(callerId);
            }

            if (!UserIsActive(callerId, database, userController.CollectionName))
            {
                throw new InsufficientPermissionsException<User>(callerId);
            }

            if (!AccountIsActive(caller.AccountId, database, accountController.CollectionName))
            {
                throw new InsufficientPermissionsException<Account>(caller.AccountId);
            }
        }

        public static void ModifyOrDeleteChannel(Guid id, Guid callerId, IDatabase database, string collectionName, UserController userController, AccountController accountController, ServerController serverController)
        {
            //? 1. Active user with active account can modify or delete channels on own server;
            //? 2. Active user with active account and permission `ManageChannels` can modify or delete channels on server;
            //? 3. Active user with active account and global permission `ManageServers` can modify or delete channels;

            if (!Exists<Channel>(id, database, collectionName))
            {
                throw new DoesNotExistException<Channel>(id);
            }

            var channel = database.Read<Channel>(collectionName, c => c.Id == id).FirstOrDefault();
            var server = database.Read<Server>(serverController.CollectionName, s => s.Id == channel.ServerId, 1).FirstOrDefault();
            var caller = database.Read<User>(userController.CollectionName, u => u.Id == callerId, 1).FirstOrDefault();

            var firstCondition = channel.ServerId == server.Id && server.OwnerId == callerId;
            var secondCondition = channel.ServerId == server.Id && channel.AuthorId == callerId && HasPermission(callerId, server.Roles, Permission.ManageServer);
            var thirdCondition = HasGlobalPermission(caller, UserGlobalPermission.ManageServers);

            if (!(firstCondition || secondCondition || thirdCondition))
            {
                throw new InsufficientPermissionsException<User>(callerId);
            }

            if (!UserIsActive(callerId, database, userController.CollectionName))
            {
                throw new InsufficientPermissionsException<User>(callerId);
            }

            if (!AccountIsActive(caller.AccountId, database, accountController.CollectionName))
            {
                throw new InsufficientPermissionsException<Account>(caller.AccountId);
            }
        }
        #endregion

        private static bool HasGlobalPermission(Guid id, AccountGlobalPermission permission, IDatabase database, string collectionName)
        {
            return database.Count<Account>(collectionName, a => a.Id == id && a.IsActive && a.GlobalPermissions.HasFlag(permission)) == 1;
        }

        private static bool HasGlobalPermission(User user, UserGlobalPermission permission)
        {
            return user.GlobalPermissions.HasFlag(permission);
        }

        public static bool HasPermission(Guid userId, List<Role> roles, Permission permission)
        {
            foreach (var role in roles)
            {
                if (role.Users.Contains(userId))
                {
                    if (role.Permission.HasFlag(permission))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool AccountIsActive(Guid id, IDatabase database, string collectionName)
        {
            return database.Count<Account>(collectionName, a => a.Id == id && a.IsActive) == 1;
        }

        private static bool UserIsActive(Guid id, IDatabase database, string collectionName)
        {
            return database.Count<User>(collectionName, a => a.Id == id && a.IsActive) == 1;
        }

        private static bool Exists<T>(Guid id, IDatabase database, string collectionName) where T : IIdentifiable
        {
            return database.Count<T>(collectionName, i => i.Id == id) == 1;
        }
    }
}