using System;
using System.Linq;
using Xunit;
using Chattle.Database;

namespace Chattle.Tests
{
    public class ModelControllerTests
    {
        public const string ConnectionString = "mongodb+srv://testUser:testPassword@cluster-ktbsc.azure.mongodb.net";
        public static IDatabase Database = new MongoDatabase(ConnectionString, "TestDatabase");

        [Fact]
        public void AccountTest()
        {
            var modelController = new Chattle(Database);

            var account = new Account("testAccount");
            modelController.AccountController.Create(account);
            modelController.AccountController.SetPassword(account.Id, "testPassword", account.Id);
            Assert.Equal(account, modelController.AccountController.Get(account.Id, account.Id));

            Assert.True(modelController.AccountController.Get(account.Id, account.Id).IsActive);

            modelController.AccountController.SetUsername(account.Id, "newUsername", account.Id);
            Assert.Equal("newUsername", modelController.AccountController.Get(account.Id, account.Id).Username);

            Assert.True(modelController.AccountController.Get(account.Id, account.Id).VerifyPassword("testPassword"));
            modelController.AccountController.SetPassword(account.Id, "TestPassword", account.Id);
            Assert.False(modelController.AccountController.Get(account.Id, account.Id).VerifyPassword("testPassword"));

            modelController.AccountController.Delete(account.Id, account.Id);
            Assert.Throws<DoesNotExistException<Account>>(() => modelController.AccountController.Get(account.Id, account.Id));
        }

        [Fact]
        public void UserTest()
        {
            var account = new Account("testAccount");
            var user1 = new User("testUser", account.Id, UserType.User);
            var user2 = new User("testUser2", account.Id, UserType.User);

            var modelController = new Chattle(Database);

            Assert.Throws<InsufficientPermissionsException<Account>>(() => modelController.UserController.Create(user1, account.Id));
            modelController.AccountController.Create(account);
            modelController.UserController.Create(user1, account.Id);

            Assert.Equal(user1, modelController.UserController.Get(user1.Id, account.Id));

            modelController.UserController.SetNickname(user1.Id, "newNickname", account.Id);
            Assert.Equal("newNickname", modelController.UserController.Get(user1.Id, account.Id).Nickname);

            modelController.AccountController.Delete(account.Id, account.Id);
            Assert.Throws<DoesNotExistException<User>>(() => modelController.UserController.Get(user1.Id, account.Id));
            Assert.Throws<DoesNotExistException<Account>>(() => modelController.AccountController.Get(account.Id, account.Id));
        }

        [Fact]
        public void ServerTest()
        {
            var modelController = new Chattle(Database);

            var account = new Account("testAccount");
            var user = new User("testUser", account.Id, UserType.User);
            var server = new Server("testServer", user.Id);

            Assert.Throws<DoesNotExistException<User>>(() => modelController.ServerController.Create(server, user.Id));
            modelController.AccountController.Create(account);
            modelController.UserController.Create(user, account.Id);
            modelController.ServerController.Create(server, user.Id);

            Assert.Equal(server, modelController.ServerController.Get(server.Id, user.Id));

            modelController.ServerController.SetName(server.Id, "newName", user.Id);
            Assert.Equal("newName", modelController.ServerController.Get(server.Id, user.Id).Name);

            modelController.ServerController.SetDescription(server.Id, "newDescription", user.Id);
            Assert.Equal("newDescription", modelController.ServerController.Get(server.Id, user.Id).Description);

            modelController.ServerController.SetName(server.Id, "newName", user.Id);
            Assert.Equal("newName", modelController.ServerController.Get(server.Id, user.Id).Name);

            modelController.AccountController.Delete(account.Id, account.Id);
            Assert.Throws<DoesNotExistException<Account>>(() => modelController.AccountController.Get(account.Id, account.Id));
            Assert.Throws<DoesNotExistException<User>>(() => modelController.UserController.Get(user.Id, account.Id));
            Assert.Throws<DoesNotExistException<Server>>(() => modelController.ServerController.Get(server.Id, user.Id));
        }

        [Fact]
        public void ChannelTest()
        {
            var modelController = new Chattle(Database);

            var account = new Account("testAccount");
            var user = new User("testUser", account.Id, UserType.User);
            var server = new Server("testServer", user.Id);
            var channel = new Channel("testChannel", server.Id, user.Id);

            Assert.Throws<DoesNotExistException<Server>>(() => modelController.ChannelController.Create(channel, user.Id));
            modelController.AccountController.Create(account);
            modelController.UserController.Create(user, account.Id);
            modelController.ServerController.Create(server, user.Id);
            modelController.ChannelController.Create(channel, user.Id);
            server.Roles.First().Users.Add(user.Id);

            Assert.Equal(channel, modelController.ChannelController.Get(channel.Id, user.Id));

            modelController.ChannelController.SetName(channel.Id, "newName", user.Id);
            Assert.Equal("newName", modelController.ChannelController.Get(channel.Id, user.Id).Name);

            modelController.ChannelController.SetDescription(channel.Id, "newDescription", user.Id);
            Assert.Equal("newDescription", modelController.ChannelController.Get(channel.Id, user.Id).Description);

            modelController.AccountController.Delete(account.Id, account.Id);
            Assert.Throws<DoesNotExistException<Account>>(() => modelController.AccountController.Get(account.Id, account.Id));
            Assert.Throws<DoesNotExistException<User>>(() => modelController.UserController.Get(user.Id, account.Id));
            Assert.Throws<DoesNotExistException<Server>>(() => modelController.ServerController.Get(server.Id, user.Id));
            Assert.Throws<DoesNotExistException<Channel>>(() => modelController.ChannelController.Get(channel.Id, user.Id));
        }

        [Fact]
        public void MessageTest()
        {
            var modelController = new Chattle(Database);

            var account = new Account("testAccount");
            var user = new User("testUser", account.Id, UserType.User);
            var server = new Server("testServer", user.Id);
            var channel = new Channel("testChannel", server.Id, user.Id);
            var message = new Message("testMessage", channel.Id, user.Id);

            Assert.Throws<DoesNotExistException<Channel>>(() => modelController.MessageController.Create(message, user.Id));
            modelController.AccountController.Create(account);
            modelController.UserController.Create(user, account.Id);
            modelController.ServerController.Create(server, user.Id);
            modelController.ChannelController.Create(channel, user.Id);
            modelController.MessageController.Create(message, user.Id);

            Assert.Equal(message, modelController.MessageController.GetMany(channel.Id, 1, user.Id).FirstOrDefault());

            modelController.MessageController.Delete(message.Id, user.Id);
            Assert.Throws<DoesNotExistException<Message>>(() => modelController.MessageController.Get(message.Id, user.Id));

            modelController.AccountController.Delete(account.Id, account.Id);
            Assert.Throws<DoesNotExistException<Account>>(() => modelController.AccountController.Get(account.Id, account.Id));
            Assert.Throws<DoesNotExistException<User>>(() => modelController.UserController.Get(user.Id, account.Id));
            Assert.Throws<DoesNotExistException<Server>>(() => modelController.ServerController.Get(server.Id, user.Id));
            Assert.Throws<DoesNotExistException<Channel>>(() => modelController.ChannelController.Get(channel.Id, user.Id));
        }
    }
}
