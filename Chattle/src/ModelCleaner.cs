using System;
using System.Linq;
using Chattle.Database;

namespace Chattle
{
    public class ModelCleaner
    {
        private IDatabase _database;
        private string _accounts;
        private string _users;
        private string _servers;
        private string _channels;
        private string _messagees;

        public ModelCleaner(IDatabase database, string accountsCollection, string usersCollection, string serversCollection, string channelsConnection, string messagesCollection)
        {
            _database = database;
            _accounts = accountsCollection;
            _users = usersCollection;
            _servers = serversCollection;
            _channels = channelsConnection;
            _messagees = messagesCollection;
        }

        public void CleanFromAccount(Guid id)
        {
            var users = _database.Read<User>(_users, u => u.AccountId == id);
            foreach (var user in users)
            {
                _database.Delete<User>(_users, user.Id);
                CleanFromUser(user.Id);
            }
        }

        public void CleanFromUser(Guid id)
        {
            var servers = _database.Read<Server>(_servers, s => s.OwnerId == id);
            foreach (var server in servers)
            {
                _database.Delete<Server>(_servers, server.Id);
                CleanFromServer(server.Id);
            }
        }

        public void CleanFromServer(Guid id)
        {
            var channels = _database.Read<Channel>(_channels, c => c.ServerId == id);
            foreach (var channel in channels)
            {
                _database.Delete<Channel>(_channels, channel.Id);
                CleanFromChannel(channel.Id);
            }
        }

        public void CleanFromChannel(Guid id)
        {
            var messages = _database.Read<Message>(_messagees, m => m.ChannelId == id);
            foreach (var message in messages)
            {
                _database.Delete<Message>(_messagees, message.Id);
            }
        }
    }
}
