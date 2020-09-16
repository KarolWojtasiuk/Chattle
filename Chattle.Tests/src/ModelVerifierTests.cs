using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace Chattle.Tests
{
    public class ModelVerifierTests
    {
        [Fact]
        public void CheckPermissionTest()
        {
            var caller = Guid.NewGuid();

            var role1 = new Role("TestRole1", Permission.None);
            var role2 = new Role("TestRole2", Permission.ManageChannels);
            var role3 = new Role("TestRole3", Permission.SendMessages);
            role1.Users.Add(caller);

            var roles = new List<Role>
            {
                role1, role2, role3
            };

            ModelVerifier.CheckPermission(caller, roles, Permission.None);
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.CheckPermission(caller, roles, Permission.ManageChannels));
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.CheckPermission(caller, roles, Permission.SendMessages));

            role3.Users.Add(caller);

            ModelVerifier.CheckPermission(caller, roles, Permission.None);
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.CheckPermission(caller, roles, Permission.ManageChannels));
            ModelVerifier.CheckPermission(caller, roles, Permission.SendMessages);

            role2.Users.Add(caller);

            ModelVerifier.CheckPermission(caller, roles, Permission.None);
            ModelVerifier.CheckPermission(caller, roles, Permission.ManageChannels);
            ModelVerifier.CheckPermission(caller, roles, Permission.SendMessages);

            role1.Users.Clear();
            role2.Users.Clear();
            role3.Users.Clear();

            Assert.Throws<ModelVerificationException>(() => ModelVerifier.CheckPermission(caller, roles, Permission.None));
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.CheckPermission(caller, roles, Permission.ManageChannels));
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.CheckPermission(caller, roles, Permission.SendMessages));
        }

        [Fact]
        public void VerifyAccountTest()
        {
            var account = new Account("TestUsername");
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.VerifyAccount(account, "test"));
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.VerifyAccount(account, "       "));
            ModelVerifier.VerifyAccount(account, "testPassword");

            account.Username = "test";
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.VerifyAccount(account, "testPassword"));
            account.Username = "      ";
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.VerifyAccount(account, "testPassword"));
            account.Username = "TestUsername";
            ModelVerifier.VerifyAccount(account, "testPassword");
        }

        [Fact]
        public void VerifyUserTest()
        {
            var user = new User("test", Guid.NewGuid(), UserType.User);
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.VerifyUser(user));

            user.Nickname = "       ";
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.VerifyUser(user));

            user.Nickname = "testNickname";
            ModelVerifier.VerifyUser(user);

            user.Image = new Uri("https://github.com");
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.VerifyUser(user));

            user.Image = new Uri("https://api.adorable.io/avatars/512/testImage");
            ModelVerifier.VerifyUser(user);
        }

        [Fact]
        public void VerifyServerTest()
        {
            var user = new User("testUser", Guid.NewGuid(), UserType.User);

            var server = new Server("test", user.Id);
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.VerifyServer(server));

            server.Name = "        ";
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.VerifyServer(server));

            server.Name = "testName";
            ModelVerifier.VerifyServer(server);

            server.Image = new Uri("https://github.com");
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.VerifyServer(server));

            server.Image = new Uri("https://api.adorable.io/avatars/512/testImage");
            ModelVerifier.VerifyServer(server);

            server.Roles.Clear();
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.VerifyServer(server));

            server.Roles.Add(new Role("newRole", Permission.ManageServer));
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.VerifyServer(server));

            server.Roles.Add(Role.CreateBasicRole());
            ModelVerifier.VerifyServer(server);

            server.Roles.FirstOrDefault().Users.Add(user.Id);
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.VerifyServer(server));

            server.Roles.LastOrDefault().Users.Add(user.Id);
            ModelVerifier.VerifyServer(server, user);
        }

        [Fact]
        public void VerifyChannelTest()
        {
            var channel = new Channel("test", Guid.NewGuid(), Guid.NewGuid());
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.VerifyChannel(channel));

            channel.Name = "       ";
            Assert.Throws<ModelVerificationException>(() => ModelVerifier.VerifyChannel(channel));

            channel.Name = "testNickname";
            ModelVerifier.VerifyChannel(channel);
        }
    }
}
