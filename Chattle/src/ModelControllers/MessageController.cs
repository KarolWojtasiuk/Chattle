using System;
using System.Linq;
using System.Collections.Generic;
using Chattle.Database;

namespace Chattle
{
    public class MessageController
    {
        private readonly IDatabase _database;
        private readonly AccountController _accountController;
        private readonly UserController _userController;
        private readonly ServerController _serverController;
        private readonly ChannelController _channelController;

        public string CollectionName { get; set; }

        public MessageController(IDatabase database, string collectionName, ModelController modelController)
        {
            _database = database;
            _accountController = modelController.AccountController;
            _userController = modelController.UserController;
            _serverController = modelController.ServerController;
            _channelController = modelController.ChannelController;
            CollectionName = collectionName;
        }

        public void Create(Message message, Guid callerId)
        {
            PermissionHelper.CreateMessage(message, callerId, _database, CollectionName, _userController, _accountController, _serverController, _channelController);
            _database.Create(CollectionName, message);
        }

        public Message Get(Guid id, Guid callerId)
        {
            PermissionHelper.GetMessage(id, callerId, _database, CollectionName, _userController, _accountController, _serverController, _channelController);
            return _database.Read<Message>(CollectionName, m => m.Id == id, 1).FirstOrDefault();
        }

        public List<Message> Get(Guid channelId, int count, Guid callerId)
        {
            PermissionHelper.GetMessages(channelId, callerId, _database, CollectionName, _userController, _accountController, _serverController, _channelController);
            return _database.Read<Message>(CollectionName, m => m.ChannelId == channelId, count).ToList();
        }

        public void Delete(Guid id, Guid callerId)
        {
            PermissionHelper.DeleteMessage(id, callerId, _database, CollectionName, _userController, _accountController, _serverController, _channelController);
            _database.Delete<Message>(CollectionName, id);
        }

        public void SetContent(Guid id, string content, Guid callerId)
        {
            PermissionHelper.ModifyMessage(id, callerId, _database, CollectionName, _userController, _accountController, _serverController, _channelController);
            _database.Update<Message>(CollectionName, id, "Content", content);
        }
    }
}
