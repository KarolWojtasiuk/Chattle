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
        DeleteMessages = 16
    }
}
