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

        public ModelController(IDatabase database, string usersCollection = "Users", string accountsCollection = "Accounts", string serversCollection = "Servers", string channelsCollection = "Channels", string messagesCollection = "Messages")
        {
            ModelCleaner = new ModelCleaner(database, usersCollection, serversCollection, channelsCollection, messagesCollection);
            AccountController = new AccountController(database, accountsCollection, ModelCleaner);
            UserController = new UserController(database, usersCollection, ModelCleaner, accountsCollection);
            ServerController = new ServerController(database, serversCollection, ModelCleaner, accountsCollection, usersCollection);
            ChannelController = new ChannelController(database, channelsCollection, ModelCleaner, accountsCollection, usersCollection, serversCollection);
            MessageController = new MessageController(database, messagesCollection, accountsCollection, usersCollection, serversCollection, channelsCollection);
        }
    }
}
