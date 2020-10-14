using Chattle.Database;

namespace Chattle
{
    public class ModelController
    {
        public AccountController AccountController;
        public UserController UserController;
        public ServerController ServerController;
        public ChannelController ChannelController;
        public MessageController MessageController;
        public ModelCleaner ModelCleaner;

        public ModelController(IDatabase database)
        {
            AccountController = new AccountController(database, "Accounts", this);
            UserController = new UserController(database, "Users", this);
            ServerController = new ServerController(database, "Servers", this);
            ChannelController = new ChannelController(database, "Channels", this);
            MessageController = new MessageController(database, "Messages", this);
            ModelCleaner = new ModelCleaner(database, UserController.CollectionName, ServerController.CollectionName, ChannelController.CollectionName, MessageController.CollectionName);
        }
    }
}
