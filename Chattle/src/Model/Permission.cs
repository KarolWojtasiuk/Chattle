using System;

namespace Chattle
{
    [Flags]
    public enum Permission
    {
        None = 0,
        ManageServer = 1,
        ManageChannels = 2,
        ReadMessages = 4,
        SendMessages = 8,
        DeleteMessages = 16,
        Administrator = ManageServer | ManageChannels | ReadMessages | SendMessages | DeleteMessages
    }

    [Flags]
    public enum AccountGlobalPermission
    {
        None = 0,
        ManageAccounts = 1,
        Administrator = ManageAccounts
    }

    [Flags]
    public enum UserGlobalPermission
    {
        None = 0,
        ManageMessages = 1,
        ManageChannels = 2,
        ManageServers = 4,
        Administrator = ManageMessages | ManageChannels | ManageServers
    }
}
