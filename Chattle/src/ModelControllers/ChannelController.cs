using System;
using System.Linq;
using Chattle.Database;

namespace Chattle
{
    public class ChannelController
    {
        private readonly IDatabase _database;
        private readonly UserController _userController;
        private readonly AccountController _accountController;
        private readonly ServerController _serverController;
        public string CollectionName { get; private set; }

        public ChannelController(IDatabase database, string collectionName, UserController userController, AccountController accountController, ServerController serverController)
        {
            _database = database;
            _userController = userController;
            _accountController = accountController;
            _serverController = serverController;
            CollectionName = collectionName;
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
            PermissionHelper.CreateChannel(channel, callerId, _database, CollectionName, _userController, _accountController, _serverController);
            _database.Create(CollectionName, channel);
        }

        public Channel Get(Guid id, Guid callerId)
        {
            PermissionHelper.GetChannel(id, callerId, _database, CollectionName, _userController, _accountController, _serverController);
            return _database.Read<Channel>(CollectionName, c => c.Id == id, 1).FirstOrDefault();
        }

        public void Delete(Guid id, Guid callerId)
        {
            PermissionHelper.ModifyOrDeleteChannel(id, callerId, _database, CollectionName, _userController, _accountController, _serverController);
            _database.Delete<Channel>(CollectionName, id);
        }

        public void SetName(Guid id, string name, Guid callerId)
        {
            VerifyName(name, id);
            PermissionHelper.ModifyOrDeleteChannel(id, callerId, _database, CollectionName, _userController, _accountController, _serverController);
            _database.Update<Channel>(CollectionName, id, "Name", name);
        }

        public void SetDescription(Guid id, string description, Guid callerId)
        {
            PermissionHelper.ModifyOrDeleteChannel(id, callerId, _database, CollectionName, _userController, _accountController, _serverController);
            _database.Update<Channel>(CollectionName, id, "Description", description);
        }
    }
}
