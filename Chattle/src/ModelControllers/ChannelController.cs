using System;
using System.Linq;
using Chattle.Database;

namespace Chattle
{
    public class ChannelController
    {
        private readonly IDatabase _database;
        private readonly string _collectionName;
        private readonly ModelCleaner _modelCleaner;
        private readonly string _accountsCollection;
        private readonly string _usersCollection;
        private readonly string _serversCollection;

        public ChannelController(IDatabase database, string collectionName, ModelCleaner modelCleaner, string accountsCollection, string usersCollection, string serversCollection)
        {
            _database = database;
            _collectionName = collectionName;
            _modelCleaner = modelCleaner;
            _accountsCollection = accountsCollection;
            _usersCollection = usersCollection;
            _serversCollection = serversCollection;
        }

        private void VerifyName(string name, Guid id)
        {
            if (name.Length < 5)
            {
                throw new ModelVerificationException<Channel>(id, "Name should be at least 5 characters long.");
            }
            else if (String.IsNullOrWhiteSpace(name))
            {
                throw new ModelVerificationException<Server>(id, "Name should not be empty or contain only whitespace.");
            }
        }

        public void Create(Channel channel, Guid callerId)
        {
            VerifyName(channel.Name, channel.Id);
            PermissionHelper.CreateChannel(channel, callerId, _database, _collectionName, _usersCollection, _accountsCollection, _serversCollection);
            _database.Create(_collectionName, channel);
        }

        public Channel Get(Guid id, Guid callerId)
        {
            PermissionHelper.GetChannel(id, callerId, _database, _collectionName, _usersCollection, _accountsCollection, _serversCollection);
            return _database.Read<Channel>(_collectionName, c => c.Id == id, 1).FirstOrDefault();
        }

        public void Delete(Guid id, Guid callerId)
        {
            PermissionHelper.ModifyOrDeleteChannel(id, callerId, _database, _collectionName, _usersCollection, _accountsCollection, _serversCollection);
            _database.Delete<Channel>(_collectionName, id);
            _modelCleaner.CleanFromChannel(id);
        }

        public void SetName(Guid id, string name, Guid callerId)
        {
            VerifyName(name, id);
            PermissionHelper.ModifyOrDeleteChannel(id, callerId, _database, _collectionName, _usersCollection, _accountsCollection, _serversCollection);
            _database.Update<Channel>(_collectionName, id, "Name", name);
        }

        public void SetDescription(Guid id, string description, Guid callerId)
        {
            PermissionHelper.ModifyOrDeleteChannel(id, callerId, _database, _collectionName, _usersCollection, _accountsCollection, _serversCollection);
            _database.Update<Channel>(_collectionName, id, "Description", description);
        }
    }
}
