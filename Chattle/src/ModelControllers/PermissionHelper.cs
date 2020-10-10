using System;
using System.Linq;
using Chattle.Database;

namespace Chattle
{
    public static class PermissionHelper
    {
        public static void CreateAccount()
        {

        }

        public static void GetAccount(Guid callerId, IDatabase database, string collectionName)
        {
            //? Any active account can get accounts;

            if (database.Count<Account>(collectionName, a => a.Id == callerId && a.IsActive) <= 0)
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }

        public static void DeleteAccount(Guid id, Guid callerId, IDatabase database, string collectionName)
        {
            //? Owner or account with global permission `ManageAccounts` can delete account;

            if (!Exists<Account>(id, database, collectionName))
            {
                throw new DoesNotExist<Account>(id);
            }

            if (id != callerId || !HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts, database, collectionName))
            {
                throw new InsufficientPermissionsException<Account>(callerId);
            }
        }

        public static void ModifyAccount(Guid id, Guid callerId, IDatabase database, string collectionName)
        {
            //? Activated owner or account with global permission `ManageAccounts` can modify account;

            if (!Exists<Account>(id, database, collectionName))
            {
                throw new DoesNotExist<Account>(id);
            }

            if (!(id == callerId && IsActive(id, database, collectionName))
            || (!HasGlobalPermission(callerId, AccountGlobalPermission.ManageAccounts, database, collectionName)))
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
