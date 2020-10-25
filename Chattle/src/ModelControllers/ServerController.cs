using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using Chattle.Database;

namespace Chattle
{
    public class ServerController
    {
        private readonly IDatabase _database;
        private readonly string _collectionName;
        private readonly ModelCleaner _modelCleaner;
        private readonly string _accountsCollection;
        private readonly string _usersCollection;

        public ServerController(IDatabase database, string collectionName, ModelCleaner modelCleaner, string accountsCollection, string usersCollection)
        {
            _database = database;
            _collectionName = collectionName;
            _modelCleaner = modelCleaner;
            _accountsCollection = accountsCollection;
            _usersCollection = usersCollection;
        }

        public void VerifyName(string name, Guid id)
        {
            if (name.Length < 5)
            {
                throw new ModelVerificationException<Server>(id, "Name should be at least 5 characters long.");
            }
            else if (String.IsNullOrWhiteSpace(name))
            {
                throw new ModelVerificationException<Server>(id, "Name should not be empty or contain only whitespace.");
            }
        }

        public void VerifyImage(Uri image, Guid id)
        {
            if (!WebRequest.Create(image).GetResponse().ContentType.ToLower().StartsWith("image/"))
            {
                throw new ModelVerificationException<Server>(id, "Image Uri should be of type `image/*`.");
            }
        }

        public void VerifyRoles(List<Role> roles, Guid id)
        {
            var defaultRole = roles.FirstOrDefault(r => r.Id == Chattle.SpecialId);

            if (defaultRole == null)
            {
                throw new ModelVerificationException<Server>(id, "Default role does not exists.");
            }

            foreach (var role in roles)
            {
                foreach (var user in role.Users)
                {
                    if (!defaultRole.Users.Contains(user))
                    {
                        throw new ModelVerificationException<Server>(id, "Assign a primary role to a user before assigning other roles to them.");
                    }
                }
            }
        }

        public void Create(Server server, Guid callerId)
        {
            VerifyName(server.Name, server.Id);
            VerifyRoles(server.Roles, server.Id);
            VerifyImage(server.Image, server.Id);
            PermissionHelper.CreateServer(server, callerId, _database, _collectionName, _usersCollection, _accountsCollection);
            _database.Create(_collectionName, server);
        }

        public Server Get(Guid id, Guid callerId)
        {
            PermissionHelper.GetServer(id, callerId, _database, _collectionName, _usersCollection, _accountsCollection);
            return _database.Read<Server>(_collectionName, s => s.Id == id, 1).FirstOrDefault();
        }

        public List<Server> GetMany(Guid callerId)
        {
            PermissionHelper.GetServer(callerId, _database, _usersCollection, _accountsCollection);
            return _database.Read<Server>(_collectionName, s => s.Roles.First(r => r.Id == Chattle.SpecialId).Users.Contains(callerId));
        }

        public void Delete(Guid id, Guid callerId)
        {
            PermissionHelper.DeleteServer(id, callerId, _database, _collectionName, _usersCollection, _accountsCollection);
            _database.Delete<Server>(_collectionName, id);
            _modelCleaner.CleanFromServer(id);
        }

        public void SetName(Guid id, string name, Guid callerId)
        {
            VerifyName(name, id);
            PermissionHelper.ModifyServer(id, callerId, _database, _collectionName, _usersCollection, _accountsCollection, _collectionName);
            _database.Update<Server>(_collectionName, id, "Name", name);
        }

        public void SetDescription(Guid id, string description, Guid callerId)
        {
            PermissionHelper.ModifyServer(id, callerId, _database, _collectionName, _usersCollection, _accountsCollection, _collectionName);
            _database.Update<Server>(_collectionName, id, "Description", description);
        }

        public void SetImage(Guid id, Uri image, Guid callerId)
        {
            VerifyImage(image, id);
            PermissionHelper.ModifyServer(id, callerId, _database, _collectionName, _usersCollection, _accountsCollection, _collectionName);
            _database.Update<Server>(_collectionName, id, "Image", image);
        }

        public void SetDefaultImage(Guid id, Guid callerId)
        {
            var image = DefaultImage.GetServerImage(id);
            PermissionHelper.ModifyServer(id, callerId, _database, _collectionName, _usersCollection, _accountsCollection, _collectionName);
            _database.Update<Server>(_collectionName, id, "Image", image);
        }

        public void SetRoles(Guid id, List<Role> roles, Guid callerId)
        {
            VerifyRoles(roles, id);
            PermissionHelper.ModifyServer(id, callerId, _database, _collectionName, _usersCollection, _accountsCollection, _collectionName);
            _database.Update<Server>(_collectionName, id, "Roles", roles);
        }
    }
}
