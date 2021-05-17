using Chattle.Database.DatabaseProviders;
using Chattle.Database.Entities;
using Serilog;

namespace Chattle.Database
{
    public class DatabaseController
    {
        public DatabaseController(IDatabaseProvider databaseProvider, ILogger logger)
        {
            Accounts = new Repository<AccountEntity>(databaseProvider, "Accounts", logger);
            Users = new Repository<UserEntity>(databaseProvider, "Users", logger);
            Servers = new Repository<ServerEntity>(databaseProvider, "Servers", logger);
            Channels = new Repository<ChannelEntity>(databaseProvider, "Channels", logger);
            Messages = new Repository<MessageEntity>(databaseProvider, "Messages", logger);
            Roles = new Repository<RoleEntity>(databaseProvider, "Roles", logger);
        }

        public Repository<AccountEntity> Accounts { get; }
        public Repository<UserEntity> Users { get; }
        public Repository<ServerEntity> Servers { get; }
        public Repository<ChannelEntity> Channels { get; }
        public Repository<MessageEntity> Messages { get; }
        public Repository<RoleEntity> Roles { get; }
    }
}