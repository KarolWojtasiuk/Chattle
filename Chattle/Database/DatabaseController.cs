using Chattle.Database.DatabaseProviders;
using Chattle.Models;
using Serilog;

namespace Chattle.Database
{
    public class DatabaseController
    {
        public DatabaseController(IDatabaseProvider databaseProvider, ILogger? logger = null)
        {
            Accounts = new Repository<Account>(databaseProvider, "Accounts", logger);
            Users = new Repository<User>(databaseProvider, "Users", logger);
            Servers = new Repository<Server>(databaseProvider, "Servers", logger);
            Channels = new Repository<Channel>(databaseProvider, "Channels", logger);
            Messages = new Repository<Message>(databaseProvider, "Messages", logger);
            Roles = new Repository<Role>(databaseProvider, "Roles", logger);
        }

        public Repository<Account> Accounts { get; }
        public Repository<User> Users { get; }
        public Repository<Server> Servers { get; }
        public Repository<Channel> Channels { get; }
        public Repository<Message> Messages { get; }
        public Repository<Role> Roles { get; }
    }
}