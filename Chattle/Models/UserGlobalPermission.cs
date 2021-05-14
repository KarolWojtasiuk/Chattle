using System;

namespace Chattle.Models
{
    [Flags]
    public enum UserGlobalPermission
    {
        None = 0,
        ManageMessages = 1 << 0,
        ManageChannels = 1 << 1,
        ManageServers = 1 << 2,
        ManageAll = ManageMessages | ManageChannels | ManageServers
    }
}