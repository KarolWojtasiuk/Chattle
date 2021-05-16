using Chattle.Database.DatabaseProviders;
using Chattle.Database.Entities;

namespace Chattle.Database
{
    public class DatabaseController
    {
        public DatabaseController(IDatabaseProvider databaseProvider)
        {
            Accounts = new Repository<AccountEntity>(databaseProvider, "Accounts");
            Users = new Repository<UserEntity>(databaseProvider, "Users");
            Servers = new Repository<ServerEntity>(databaseProvider, "Servers");
            Channels = new Repository<ChannelEntity>(databaseProvider, "Channels");
            Messages = new Repository<MessageEntity>(databaseProvider, "Messages");
            Roles = new Repository<RoleEntity>(databaseProvider, "Roles");
        }

        public Repository<AccountEntity> Accounts { get; }
        public Repository<UserEntity> Users { get; }
        public Repository<ServerEntity> Servers { get; }
        public Repository<ChannelEntity> Channels { get; }
        public Repository<MessageEntity> Messages { get; }
        public Repository<RoleEntity> Roles { get; }
    }
}