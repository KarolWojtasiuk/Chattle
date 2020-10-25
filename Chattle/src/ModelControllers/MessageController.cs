using System;
using System.Linq;
using System.Collections.Generic;
using Chattle.Database;

namespace Chattle
{
    public class MessageController
    {
        private readonly IDatabase _database;
        private readonly string _collectionName;
        private readonly string _accountsCollection;
        private readonly string _usersCollection;
        private readonly string _serversCollection;
        private readonly string _channelsCollection;

        public MessageController(IDatabase database, string collectionName, string accountsCollection, string usersCollection, string serversCollection, string channelsCollection)
        {
            _database = database;
            _collectionName = collectionName;
            _accountsCollection = accountsCollection;
            _usersCollection = usersCollection;
            _serversCollection = serversCollection;
            _channelsCollection = channelsCollection;
        }

        public void Create(Message message, Guid callerId)
        {
            PermissionHelper.CreateMessage(message, callerId, _database, _collectionName, _usersCollection, _accountsCollection, _serversCollection, _channelsCollection);
            _database.Create(_collectionName, message);
        }

        public Message Get(Guid id, Guid callerId)
        {
            PermissionHelper.GetMessage(id, callerId, _database, _collectionName, _usersCollection, _accountsCollection, _serversCollection, _channelsCollection);
            return _database.Read<Message>(_collectionName, m => m.Id == id, 1).FirstOrDefault();
        }

        public List<Message> GetMany(Guid channelId, int count, Guid callerId)
        {
            PermissionHelper.GetMessage(channelId, callerId, _database, _usersCollection, _accountsCollection, _serversCollection, _channelsCollection);
            return _database.Read<Message>(_collectionName, m => m.ChannelId == channelId, count).ToList();
        }

        public void Delete(Guid id, Guid callerId)
        {
            PermissionHelper.DeleteMessage(id, callerId, _database, _collectionName, _usersCollection, _accountsCollection, _serversCollection, _channelsCollection);
            _database.Delete<Message>(_collectionName, id);
        }

        public void SetContent(Guid id, string content, Guid callerId)
        {
            PermissionHelper.ModifyMessage(id, callerId, _database, _collectionName, _usersCollection, _accountsCollection, _serversCollection, _channelsCollection);
            _database.Update<Message>(_collectionName, id, "Content", content);
        }
    }
}
