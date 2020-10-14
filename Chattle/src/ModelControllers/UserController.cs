using System;
using System.Net;
using System.Linq;
using Chattle.Database;

namespace Chattle
{
    public class UserController
    {
        private readonly IDatabase _database;
        private readonly string _collectionName;
        private readonly ModelCleaner _modelCleaner;
        private readonly string _accountsCollection;

        public UserController(IDatabase database, string collectionName, ModelCleaner modelCleaner, string accountsCollection)
        {
            _database = database;
            _collectionName = collectionName;
            _modelCleaner = modelCleaner;
            _accountsCollection = accountsCollection;
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
            PermissionHelper.CreateUser(user, callerId, _database, _collectionName, _accountsCollection);
            _database.Create(_collectionName, user);
        }

        public User Get(Guid id, Guid callerId)
        {
            PermissionHelper.GetUser(id, callerId, _database, _collectionName, _accountsCollection);
            return _database.Read<User>(_collectionName, u => u.Id == id, 1).First();
        }

        public void Delete(Guid id, Guid callerId)
        {
            PermissionHelper.DeleteUser(id, callerId, _database, _collectionName, _accountsCollection);
            _database.Delete<User>(_collectionName, id);
            _modelCleaner.CleanFromUser(id);
        }

        public void SetActive(Guid id, bool active, Guid callerId)
        {
            PermissionHelper.ManageUser(id, callerId, _database, _collectionName, _accountsCollection);
            _database.Update<User>(_collectionName, id, "IsActive", active);
        }

        public void SetGlobalPermissions(Guid id, UserGlobalPermission permissions, Guid callerId)
        {
            PermissionHelper.ManageUser(id, callerId, _database, _collectionName, _accountsCollection);
            _database.Update<User>(_collectionName, id, "GlobalPermissions", permissions);
        }

        public void SetNickname(Guid id, string nickname, Guid callerId)
        {
            VerifyNickname(nickname, id);
            PermissionHelper.ModifyUser(id, callerId, _database, _collectionName, _accountsCollection);
            _database.Update<User>(_collectionName, id, "Nickname", nickname);
        }

        public void SetImage(Guid id, Uri image, Guid callerId)
        {
            VerifyImage(image, id);
            PermissionHelper.ModifyUser(id, callerId, _database, _collectionName, _accountsCollection);
            _database.Update<User>(_collectionName, id, "Image", image);
        }

        public void SetDefaultImage(Guid id, Guid callerId)
        {
            var image = DefaultImage.GetUserImage(id);
            PermissionHelper.ModifyUser(id, callerId, _database, _collectionName, _accountsCollection);
            _database.Update<User>(_collectionName, id, "Image", image);
        }
    }
}
