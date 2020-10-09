using System;
using System.Collections.Generic;
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

        public ModelController(IDatabase database)
        {
            AccountController = new AccountController(database, "Accounts");
            UserController = new UserController(database, "Users");
            ServerController = new ServerController(database, "Servers");
            ChannelController = new ChannelController(database, "Channels");
            MessageController = new MessageController(database, "Messages");
        }

    }
}
