using System;

namespace Chattle.Models
{
    [Flags]
    public enum AccountGlobalPermission
    {
        None = 0,
        ManageUsers = 1 << 0,
        ManageAccounts = 1 << 1,
        ManageAll = ManageUsers | ManageAccounts
    }
}