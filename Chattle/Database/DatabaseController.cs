using Chattle.Database.Entities;

namespace Chattle.Database
{
    public class DatabaseController
    {
        public DatabaseController(IDatabaseContext databaseContext)
        {
            Accounts = new Repository<AccountEntity>(databaseContext, "Accounts");
            Users = new Repository<UserEntity>(databaseContext, "Users");
            Servers = new Repository<ServerEntity>(databaseContext, "Servers");
            Channels = new Repository<ChannelEntity>(databaseContext, "Channels");
            Messages = new Repository<MessageEntity>(databaseContext, "Messages");
            Roles = new Repository<RoleEntity>(databaseContext, "Roles");
        }

        public Repository<AccountEntity> Accounts { get; }
        public Repository<UserEntity> Users { get; }
        public Repository<ServerEntity> Servers { get; }
        public Repository<ChannelEntity> Channels { get; }
        public Repository<MessageEntity> Messages { get; }
        public Repository<RoleEntity> Roles { get; }
    }
}