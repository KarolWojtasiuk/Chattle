using System;
using System.Linq;
using Xunit;
using Chattle.Database;

namespace Chattle.Tests
{
    public class ModelControllerTests
    {
        public const string ConnectionString = "mongodb+srv://testUser:testPassword@cluster-ktbsc.azure.mongodb.net";
        public static IDatabase Database1 = new MongoDatabase(ConnectionString, "TestDatabase1");
        public static IDatabase Database2 = new MongoDatabase(ConnectionString, "TestDatabase2");

        [Fact]
        public void DatabasesTest()
        {
            var account = new Account("testAccount");
            var modelController = new ModelController(Database1);
            modelController.Databases.Add(Database2);

            modelController.CreateAccount(account);
            Assert.Equal(account, Database1.Read<Account>("Accounts", a => a.Id == account.Id).FirstOrDefault());
            Assert.Equal(account, Database2.Read<Account>("Accounts", a => a.Id == account.Id).FirstOrDefault());
            Assert.Equal(account, modelController.FindAccount(account.Id));

            modelController.DeleteAccount(account);
            Assert.Null(modelController.FindAccount(account.Id));
            Assert.Null(Database1.Read<Account>("Accounts", a => a.Id == account.Id).FirstOrDefault());
            Assert.Null(Database2.Read<Account>("Accounts", a => a.Id == account.Id).FirstOrDefault());
        }
        [Fact]
        public void AccountTest()
        {
            var account = new Account("testAccount");
            account.ChangePassword("testPassword");

            var modelController = new ModelController(Database1);

            modelController.CreateAccount(account);
            Assert.Equal(account, modelController.FindAccount(account.Id));

            modelController.DeactivateAccount(account);
            Assert.False(modelController.FindAccount(account.Id).IsActive);

            modelController.ActivateAccount(account);
            Assert.True(modelController.FindAccount(account.Id).IsActive);

            modelController.ChangeAccountUsername(account, "newUsername");
            Assert.Equal("newUsername", modelController.FindAccount(account.Id).Username);

            Assert.True(modelController.FindAccount(account.Id).VerifyPassword("testPassword"));
            modelController.ChangeAccountPassword(account, "TestPassword");
            Assert.False(modelController.FindAccount(account.Id).VerifyPassword("testPassword"));

            modelController.DeleteAccount(account);
            Assert.Null(modelController.FindAccount(account.Id));
        }

        [Fact]
        public void UserTest()
        {
            var account = new Account("testAccount");
            var user1 = new User("testUser", account.Id, UserType.User);
            var user2 = new User("testUser2", account.Id, UserType.User);

            var modelController = new ModelController(Database1);

            Assert.Throws<DoesNotExistsException>(() => modelController.CreateUser(user1));
            modelController.CreateAccount(account);
            modelController.CreateUser(user1);

            Assert.Equal(user1, modelController.FindUser(user1.Id));

            modelController.DeactivateUser(user1);
            Assert.False(modelController.FindUser(user1.Id).IsActive);

            modelController.ActivateUser(user1);
            Assert.True(modelController.FindUser(user1.Id).IsActive);

            modelController.ChangeUserNickname(user1, "newNickname");
            Assert.Equal("newNickname", modelController.FindUser(user1.Id).Nickname);

            modelController.ChangeUserImage(user1, new Uri("https://api.adorable.io/avatars/512/TestUser"));
            Assert.Equal(new Uri("https://api.adorable.io/avatars/512/TestUser"), modelController.FindUser(user1.Id).Image);

            modelController.RestoreDefaultUserImage(user1);
            Assert.Equal(DefaultImage.GetUserImage(user1.Id), modelController.FindUser(user1.Id).Image);

            modelController.DeleteUser(user1);
            modelController.DeleteAccount(account);
            Assert.Null(modelController.FindUser(user1.Id));
            Assert.Null(modelController.FindAccount(account.Id));
        }

        [Fact]
        public void ServerTest()
        {
            var modelController = new ModelController(Database1);

            var account = new Account("testAccount");
            var user = new User("testUser", account.Id, UserType.User);
            var server = new Server("testServer", user.Id);

            Assert.Throws<DoesNotExistsException>(() => modelController.CreateServer(server));
            modelController.CreateAccount(account);
            modelController.CreateUser(user);
            modelController.CreateServer(server);

            Assert.Equal(server, modelController.FindServer(server.Id));

            modelController.ChangeServerName(server, "newName", user.Id);
            Assert.Equal("newName", modelController.FindServer(server.Id).Name);

            modelController.ChangeServerDescription(server, "newDescription", user.Id);
            Assert.Equal("newDescription", modelController.FindServer(server.Id).Description);

            modelController.ChangeServerImage(server, new Uri("https://api.adorable.io/avatars/512/TestServer"), user.Id);
            Assert.Equal(new Uri("https://api.adorable.io/avatars/512/TestServer"), modelController.FindServer(server.Id).Image);

            modelController.ChangeServerName(server, "newName", user.Id);
            Assert.Equal("newName", modelController.FindServer(server.Id).Name);

            modelController.ChangeServerRoles(server, user.Id);

            modelController.DeleteServer(server, user.Id);
            modelController.DeleteUser(user);
            modelController.DeleteAccount(account);
            Assert.Null(modelController.FindUser(user.Id));
            Assert.Null(modelController.FindAccount(account.Id));
            Assert.Null(modelController.FindServer(server.Id));
        }

        [Fact]
        public void ChannelTest()
        {
            var modelController = new ModelController(Database1);

            var account = new Account("testAccount");
            var user = new User("testUser", account.Id, UserType.User);
            var server = new Server("testServer", user.Id);
            var channel = new Channel("testChannel", server.Id, user.Id);

            Assert.Throws<DoesNotExistsException>(() => modelController.CreateChannel(channel, user.Id));
            modelController.CreateAccount(account);
            modelController.CreateUser(user);
            modelController.CreateServer(server);
            modelController.CreateChannel(channel, user.Id);

            Assert.Equal(channel, modelController.FindChannel(channel.Id));

            modelController.ChangeChannelName(channel, "newName", user.Id);
            Assert.Equal("newName", modelController.FindChannel(channel.Id).Name);

            modelController.ChangeChannelDescription(channel, "newDescription", user.Id);
            Assert.Equal("newDescription", modelController.FindChannel(channel.Id).Description);

            modelController.DeleteChannel(channel, user.Id);
            modelController.DeleteServer(server, user.Id);
            modelController.DeleteUser(user);
            modelController.DeleteAccount(account);
            Assert.Null(modelController.FindUser(user.Id));
            Assert.Null(modelController.FindAccount(account.Id));
            Assert.Null(modelController.FindServer(server.Id));
            Assert.Null(modelController.FindChannel(channel.Id));
        }

        [Fact]
        public void MessageTest()
        {
            var modelController = new ModelController(Database1);

            var account = new Account("testAccount");
            var user = new User("testUser", account.Id, UserType.User);
            var server = new Server("testServer", user.Id);
            var channel = new Channel("testChannel", server.Id, user.Id);
            var message = new Message("testMessage", channel.Id, user.Id);

            Assert.Throws<DoesNotExistsException>(() => modelController.CreateMessage(message));
            modelController.CreateAccount(account);
            modelController.CreateUser(user);
            modelController.CreateServer(server);
            modelController.CreateChannel(channel, user.Id);
            modelController.CreateMessage(message);

            Assert.Equal(message, modelController.FindMessages(channel.Id, 1, user.Id).FirstOrDefault());

            modelController.DeleteMessage(message, user.Id);
            Assert.Null(modelController.FindMessages(channel.Id, 1, user.Id).FirstOrDefault(m => m.Id == message.Id));

            modelController.DeleteChannel(channel, user.Id);
            modelController.DeleteServer(server, user.Id);
            modelController.DeleteUser(user);
            modelController.DeleteAccount(account);
            Assert.Null(modelController.FindUser(user.Id));
            Assert.Null(modelController.FindAccount(account.Id));
            Assert.Null(modelController.FindServer(server.Id));
            Assert.Null(modelController.FindChannel(channel.Id));
        }
    }
}
