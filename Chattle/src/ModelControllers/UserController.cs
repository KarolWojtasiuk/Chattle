using System;
using System.Net;
using System.Linq;
using Chattle.Database;

namespace Chattle
{
    public class UserController
    {
        private readonly IDatabase _database;
        private readonly AccountController _accountController;
        public string CollectionName { get; private set; }

        public UserController(IDatabase database, string collectionName, AccountController accountController)
        {
            _database = database;
            _accountController = accountController;
            CollectionName = collectionName;
        }

        private void VerifyNickname(string nickname, Guid id)
        {
            if (nickname.Length < 5)
            {
                throw new ModelVerificationException<User>(id, "Nickname should be at least 5 characters long.");
            }
            else if (String.IsNullOrWhiteSpace(nickname))
            {
                throw new ModelVerificationException<User>(id, "Nickname should not be empty or contain only whitespace.");
            }
        }

        private void VerifyImage(Uri image, Guid id)
        {
            if (!WebRequest.Create(image).GetResponse().ContentType.ToLower().StartsWith("image/"))
            {
                throw new ModelVerificationException<User>(id, "Image Uri should be of type `image/*`.");
            }
        }

        public void Create(User user, Guid callerId)
        {
            VerifyNickname(user.Nickname, user.Id);
            VerifyImage(user.Image, user.Id);
            PermissionHelper.CreateUser(user, callerId, _database, CollectionName, _accountController);
            _database.Create(CollectionName, user);
        }

        public User Get(Guid id, Guid callerId)
        {
            PermissionHelper.GetUser(id, callerId, _database, CollectionName, _accountController);
            return _database.Read<User>(CollectionName, u => u.Id == id, 1).First();
        }

        public void Delete(Guid id, Guid callerId)
        {
            PermissionHelper.DeleteUser(id, callerId, _database, CollectionName, _accountController);
            _database.Delete<User>(CollectionName, id);
        }

        public void SetActive(Guid id, bool active, Guid callerId)
        {
            PermissionHelper.ManageUser(id, callerId, _database, CollectionName, _accountController);
            _database.Update<User>(CollectionName, id, "IsActive", active);
        }

        public void SetGlobalPermissions(Guid id, UserGlobalPermission permissions, Guid callerId)
        {
            PermissionHelper.ManageUser(id, callerId, _database, CollectionName, _accountController);
            _database.Update<User>(CollectionName, id, "GlobalPermissions", permissions);
        }

        public void SetNickname(Guid id, string nickname, Guid callerId)
        {
            VerifyNickname(nickname, id);
            PermissionHelper.ModifyUser(id, callerId, _database, CollectionName, _accountController);
            _database.Update<User>(CollectionName, id, "Nickname", nickname);
        }

        public void SetImage(Guid id, Uri image, Guid callerId)
        {
            VerifyImage(image, id);
            PermissionHelper.ModifyUser(id, callerId, _database, CollectionName, _accountController);
            _database.Update<User>(CollectionName, id, "Image", image);
        }
    }
}
