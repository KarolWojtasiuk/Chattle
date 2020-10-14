using System;
using System.Drawing;
using Xunit;

namespace Chattle.Tests
{
    public class ModelTests
    {

        [Fact]
        public void AccountTest()
        {
            var testUsername = "TestUsername";
            var testPassword = "TestPassword";
            var testAccount = new Account(testUsername);
            testAccount.ChangePassword(testPassword);

            Assert.NotEqual(Guid.Empty, testAccount.Id);
            Assert.Equal(testUsername, testAccount.Username);
            Assert.True(testAccount.IsActive);
            Assert.NotEqual(new DateTime(), testAccount.CreationTime);

            Assert.True(testAccount.VerifyPassword(testPassword));
            Assert.False(testAccount.VerifyPassword(testPassword.ToLower()));
            testAccount.ChangePassword(testPassword.ToUpper());
            Assert.True(testAccount.VerifyPassword(testPassword.ToUpper()));
            Assert.False(testAccount.VerifyPassword(testPassword));
        }

        [Fact]
        public void UserTest()
        {
            var testNickname = "TestNickname";
            var testUser = new User(testNickname, Guid.NewGuid(), UserType.User);
            var testBot = new User(testNickname, Guid.NewGuid(), UserType.Bot);

            Assert.NotEqual(Guid.Empty, testUser.Id);
            Assert.NotEqual(Guid.Empty, testBot.Id);
            Assert.Equal(testNickname, testUser.Nickname);
            Assert.Equal(testNickname, testBot.Nickname);
            Assert.True(testUser.IsActive);
            Assert.True(testBot.IsActive);
            Assert.Equal(UserType.User, testUser.Type);
            Assert.Equal(UserType.Bot, testBot.Type);
            Assert.NotEqual(new DateTime(), testUser.CreationTime);
            Assert.NotEqual(new DateTime(), testBot.CreationTime);
        }

        [Fact]
        public void ServerTest()
        {
            var testName = "TestName";
            var testDescription = "TestDescription";
            var testServer2 = new Server(testName, Guid.NewGuid());
            var testServer1 = new Server(testName, Guid.NewGuid(), testDescription);

            Assert.NotEqual(Guid.Empty, testServer1.Id);
            Assert.NotEqual(Guid.Empty, testServer2.Id);
            Assert.Equal(testName, testServer1.Name);
            Assert.Equal(testName, testServer2.Name);
            Assert.Equal(String.Empty, testServer2.Description);
            Assert.Equal(testDescription, testServer1.Description);
            Assert.Contains(testServer1.Roles, x => x.Id == Guid.Empty);
            Assert.Contains(testServer2.Roles, x => x.Id == Guid.Empty);
            Assert.NotEqual(new DateTime(), testServer1.CreationTime);
            Assert.NotEqual(new DateTime(), testServer2.CreationTime);
        }

        [Fact]
        public void ChannelTest()
        {
            var testName = "TestChannel";
            var testDescription = "TestDescription";

            var testChannel1 = new Channel(testName, Guid.NewGuid(), Guid.NewGuid());
            var testChannel2 = new Channel(testName, Guid.NewGuid(), Guid.NewGuid(), testDescription);

            Assert.NotEqual(Guid.Empty, testChannel1.Id);
            Assert.NotEqual(Guid.Empty, testChannel2.Id);
            Assert.Equal(testName, testChannel1.Name);
            Assert.Equal(testName, testChannel2.Name);
            Assert.Equal(String.Empty, testChannel1.Description);
            Assert.Equal(testDescription, testChannel2.Description);
            Assert.NotEqual(new DateTime(), testChannel1.CreationTime);
            Assert.NotEqual(new DateTime(), testChannel2.CreationTime);
        }

        [Fact]
        public void MessageTest()
        {
            var testContent = "TestContent";
            var testMessage = new Message(testContent, Guid.NewGuid(), Guid.NewGuid());

            Assert.NotEqual(Guid.Empty, testMessage.Id);
            Assert.Equal(testContent, testMessage.Content);
            Assert.NotEqual(Guid.Empty, testMessage.ChannelId);
            Assert.NotEqual(Guid.Empty, testMessage.UserId);
            Assert.NotEqual(new DateTime(), testMessage.CreationTime);
        }

        [Fact]
        public void RoleTest()
        {
            var testName = "TestName";
            var testRole1 = new Role(testName, Permission.None);
            var testRole2 = new Role(testName, Permission.None, Color.FromArgb(255, 255, 255));

            Assert.NotEqual(Guid.Empty, testRole1.Id);
            Assert.NotEqual(Guid.Empty, testRole2.Id);
            Assert.Equal(testName, testRole1.Name);
            Assert.Equal(testName, testRole2.Name);
            Assert.Equal("#000000", testRole1.Color.ToUpper());
            Assert.Equal("#FFFFFF", testRole2.Color.ToUpper());

            testRole1.ChangeColor(Color.Fuchsia);
            testRole2.ChangeColor(Color.Gold);
            Assert.Equal("#FF00FF", testRole1.Color);
            Assert.Equal("#FFD700", testRole2.Color);

            Assert.Empty(testRole1.Users);
            Assert.Empty(testRole2.Users);
            Assert.NotEqual(new DateTime(), testRole1.CreationTime);
            Assert.NotEqual(new DateTime(), testRole2.CreationTime);
        }
    }
}
