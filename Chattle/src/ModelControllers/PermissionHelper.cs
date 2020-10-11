using System;
using System.Linq;
using Chattle.Database;

namespace Chattle
{
    public static class PermissionHelper
    {
        #region Account
        public static void CreateAccount()
        {
            //? Anyone can create an account;
        }

        public static void GetAccount(Guid callerId, IDatabase database, string collectionName)
        {
            //? Any active account can get accounts;

            if (IsActive(callerId, database, collectionName))
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }

        public static void DeleteAccount(Guid id, Guid callerId, IDatabase database, string collectionName)
        {
            //? 1. Owner can delete own account;
            //? 2. Account with global permission `ManageAccounts` can delete other accounts;

            if (!Exists<Account>(id, database, collectionName))
            {
                throw new DoesNotExist<Account>(id);
            }

            var firstCondition = id != callerId;
            var secondCondition = !HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts, database, collectionName);

            if (firstCondition || secondCondition)
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }

        public static void ModifyAccount(Guid id, Guid callerId, IDatabase database, string collectionName)
        {
            //? 1. Activated owner can modify own account;
            //? 2. Account with global permission `ManageAccounts` can modify other accounts;

            if (!Exists<Account>(id, database, collectionName))
            {
                throw new DoesNotExist<Account>(id);
            }

            var firstCondition = !(id == callerId && IsActive(id, database, collectionName));
            var secondCondition = !HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts, database, collectionName);

            if (firstCondition || secondCondition)
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }

        public static void ManageAccount(Guid id, Guid callerId, IDatabase database, string collectionName)
        {
            //? Only account with global permission `ManageAccounts` can delete account;

            if (!Exists<Account>(id, database, collectionName))
            {
                throw new DoesNotExist<Account>(id);
            }

            if (!HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts, database, collectionName))
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }
        #endregion

        #region User
        public static void CreateUser(User user, Guid callerId, IDatabase database, string collectionName, AccountController accountController)
        {
            //? 1. Activated account can create user for own account;
            //? 2. Account with global permission `ManageAccounts` can create users for other accounts;
            //? Limited to one `UserType.User` per account;

            var firstCondition = !(user.AccountId == callerId && IsActive(user.AccountId, database, accountController.CollectionName));
            var secondCondition = !HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts, database, accountController.CollectionName);

            if (user.Type == UserType.User)
            {
                if (database.Count<User>(collectionName, u => u.AccountId == user.AccountId) > 0)
                {
                    throw new ModelVerificationException<Account>(user.AccountId, "Only one user can be assigned to an account.");
                }
            }

            if (firstCondition || secondCondition)
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }

        public static void GetUser(Guid callerId, IDatabase database, AccountController accountController)
        {
            //? Any active account can get users;

            if (IsActive(callerId, database, accountController.CollectionName))
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }

        public static void DeleteUser(Guid id, Guid callerId, IDatabase database, string collectionName, AccountController accountController)
        {
            //? 1. Owner can delete own users;
            //? 2. Account with global permission `ManageAccounts` can delete other accounts;

            if (!Exists<User>(id, database, collectionName))
            {
                throw new DoesNotExist<Account>(id);
            }

            var firstCondition = id != callerId;
            var secondCondition = !HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts, database, accountController.CollectionName);

            if (firstCondition || secondCondition)
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }

        public static void ModifyUser(Guid id, Guid callerId, IDatabase database, string collectionName, AccountController accountController)
        {
            //? 1. Activated account can modify own users;
            //? 2. Account with global permission `ManageAccounts` can modify other users;

            if (!Exists<User>(id, database, collectionName))
            {
                throw new DoesNotExist<User>(id);
            }

            var firstCondition = !(id == callerId && IsActive(id, database, accountController.CollectionName));
            var secondCondition = !HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts, database, accountController.CollectionName);

            if (firstCondition || secondCondition)
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }

        public static void ManageUser(Guid id, Guid callerId, IDatabase database, string collectionName, AccountController accountController)
        {
            //? Only account with global permission `ManageAccounts` can delete user;

            if (!Exists<User>(id, database, collectionName))
            {
                throw new DoesNotExist<User>(id);
            }

            if (!HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts, database, accountController.CollectionName))
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }
        #endregion

        private static bool HasGlobalPermission(Guid id, AccountGlobalPermission permission, IDatabase database, string collectionName)
        {
            return database.Count<Account>(collectionName, a => a.Id == id && a.IsActive && a.GlobalPermissions.HasFlag(permission)) == 1;
        }

        private static bool IsActive(Guid id, IDatabase database, string collectionName)
        {
            return database.Count<Account>(collectionName, a => a.Id == id && a.IsActive) == 1;
        }

        private static bool Exists<T>(Guid id, IDatabase database, string collectionName) where T : IIdentifiable
        {
            return database.Count<T>(collectionName, i => i.Id == id) == 1;
        }
    }
}
