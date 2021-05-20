using System;

namespace Chattle.Models
{
    [Flags]
    public enum Permission
    {
        None = 0,
        ReadMessages = 1 << 0,
        SendMessages = 1 << 1,
        DeleteMessages = 1 << 2,
        ManageChannels = 1 << 3,
        ManageServer = 1 << 4,
        Administrator = ReadMessages | SendMessages | DeleteMessages | ManageChannels | ManageServer
    }
}