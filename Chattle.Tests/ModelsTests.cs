using Chattle.Models;
using Xunit;

namespace Chattle.Tests
{
    public class ModelsTests
    {
        [Fact]
        public void AccountGlobalPermissionTest()
        {
            var permission1 = AccountGlobalPermission.ManageAll;
            var permission2 = AccountGlobalPermission.ManageAccounts;
            var permission3 = AccountGlobalPermission.None;

            Assert.True(permission1.HasFlag(AccountGlobalPermission.None));
            Assert.True(permission1.HasFlag(AccountGlobalPermission.ManageUsers));
            Assert.True(permission1.HasFlag(AccountGlobalPermission.ManageAccounts));
            Assert.True(permission1.HasFlag(AccountGlobalPermission.ManageAll));

            Assert.True(permission2.HasFlag(AccountGlobalPermission.None));
            Assert.True(permission2.HasFlag(AccountGlobalPermission.ManageAccounts));
            Assert.False(permission2.HasFlag(AccountGlobalPermission.ManageUsers));
            Assert.False(permission2.HasFlag(AccountGlobalPermission.ManageAll));

            Assert.True(permission3.HasFlag(AccountGlobalPermission.None));
            Assert.False(permission3.HasFlag(AccountGlobalPermission.ManageUsers));
            Assert.False(permission3.HasFlag(AccountGlobalPermission.ManageAccounts));
            Assert.False(permission2.HasFlag(AccountGlobalPermission.ManageAll));
        }

        [Fact]
        public void UserGlobalPermissionTest()
        {
            var permission1 = UserGlobalPermission.ManageAll;
            var permission2 = UserGlobalPermission.ManageServers;
            var permission3 = UserGlobalPermission.None;

            Assert.True(permission1.HasFlag(UserGlobalPermission.None));
            Assert.True(permission1.HasFlag(UserGlobalPermission.ManageServers));
            Assert.True(permission1.HasFlag(UserGlobalPermission.ManageChannels));
            Assert.True(permission1.HasFlag(UserGlobalPermission.ManageAll));

            Assert.True(permission2.HasFlag(UserGlobalPermission.None));
            Assert.True(permission2.HasFlag(UserGlobalPermission.ManageServers));
            Assert.False(permission2.HasFlag(UserGlobalPermission.ManageChannels));
            Assert.False(permission2.HasFlag(UserGlobalPermission.ManageAll));

            Assert.True(permission3.HasFlag(UserGlobalPermission.None));
            Assert.False(permission3.HasFlag(UserGlobalPermission.ManageServers));
            Assert.False(permission3.HasFlag(UserGlobalPermission.ManageChannels));
            Assert.False(permission2.HasFlag(UserGlobalPermission.ManageAll));
        }

        [Fact]
        public void PermissionTest()
        {
            var permission1 = Permission.Administrator;
            var permission2 = Permission.ManageServer;
            var permission3 = Permission.None;

            Assert.True(permission1.HasFlag(Permission.None));
            Assert.True(permission1.HasFlag(Permission.ManageServer));
            Assert.True(permission1.HasFlag(Permission.ManageChannels));
            Assert.True(permission1.HasFlag(Permission.Administrator));

            Assert.True(permission2.HasFlag(Permission.None));
            Assert.True(permission2.HasFlag(Permission.ManageServer));
            Assert.False(permission2.HasFlag(Permission.ManageChannels));
            Assert.False(permission2.HasFlag(Permission.Administrator));

            Assert.True(permission3.HasFlag(Permission.None));
            Assert.False(permission3.HasFlag(Permission.ManageServer));
            Assert.False(permission3.HasFlag(Permission.ManageChannels));
            Assert.False(permission2.HasFlag(Permission.Administrator));
        }

        [Fact]
        public void ColorTest()
        {
            Assert.Equal(255, Color.White.R);
            Assert.Equal(255, Color.White.G);
            Assert.Equal(255, Color.White.B);
            Assert.Equal("#ffffff", Color.White.ToHexString().ToLower());
        }
    }
}