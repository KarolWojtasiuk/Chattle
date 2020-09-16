using System;

namespace Chattle
{
    [Flags]
    public enum Permission
    {
        None = 0,
        ManageServer = 1,
        ManageChannels = 2
    }
}
