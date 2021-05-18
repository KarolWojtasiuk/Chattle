using System;
using System.Collections.Generic;
using Chattle.Database.Entities;
using Chattle.Models;
using Xunit;

namespace Chattle.Tests.Database
{
    public class EntitiesTests
    {
        [Fact]
        public void AccountTest()
        {
            var account = new AccountEntity
            {
                Id = Guid.NewGuid(),
                Name = "Account",
                IsActive = true,
                PasswordHash = ".",
                PasswordSalt = ".",
                GlobalPermission = AccountGlobalPermission.ManageAll,
                CreationDate = DateTime.UtcNow
            };

            Assert.NotNull(account);
            Assert.NotEqual(Guid.Empty, account.Id);
            Assert.NotNull(account.Name);
            Assert.True(account.IsActive);
            Assert.NotNull(account.PasswordHash);
            Assert.NotNull(account.PasswordSalt);
            Assert.NotEqual(AccountGlobalPermission.None, account.GlobalPermission);
            Assert.NotEqual(new DateTime(), account.CreationDate);
        }

        [Fact]
        public void UserTest()
        {
            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Nickname = "User",
                Status = "Status",
                IsActive = true,
                ImageUri = new Uri("https://127.0.0.1"),
                AccountId = Guid.NewGuid(),
                UserType = UserType.Bot,
                GlobalPermission = UserGlobalPermission.ManageAll,
                CreationDate = DateTime.UtcNow
            };

            Assert.NotNull(user);
            Assert.NotEqual(Guid.Empty, user.Id);
            Assert.NotNull(user.Nickname);
            Assert.NotNull(user.Status);
            Assert.True(user.IsActive);
            Assert.NotNull(user.ImageUri);
            Assert.NotEqual(Guid.Empty, user.AccountId);
            Assert.NotEqual(UserType.User, user.UserType);
            Assert.NotEqual(UserGlobalPermission.None, user.GlobalPermission);
            Assert.NotEqual(new DateTime(), user.CreationDate);
        }

        [Fact]
        public void ServerTest()
        {
            var server = new ServerEntity
            {
                Id = Guid.NewGuid(),
                Name = "Server",
                Description = "Description",
                ImageUri = new Uri("https://127.0.0.1"),
                OwnerId = Guid.NewGuid(),
                CreationDate = DateTime.UtcNow
            };

            Assert.NotNull(server);
            Assert.NotEqual(Guid.Empty, server.Id);
            Assert.NotNull(server.Name);
            Assert.NotNull(server.Description);
            Assert.NotNull(server.ImageUri);
            Assert.NotEqual(Guid.Empty, server.OwnerId);
            Assert.NotEqual(new DateTime(), server.CreationDate);
        }

        [Fact]
        public void ChannelTest()
        {
            var channel = new ChannelEntity
            {
                Id = Guid.NewGuid(),
                Name = "Server",
                Description = "Description",
                ServerId = Guid.NewGuid(),
                AuthorId = Guid.NewGuid(),
                CreationDate = DateTime.UtcNow
            };

            Assert.NotNull(channel);
            Assert.NotEqual(Guid.Empty, channel.Id);
            Assert.NotNull(channel.Name);
            Assert.NotNull(channel.Description);
            Assert.NotEqual(Guid.Empty, channel.ServerId);
            Assert.NotEqual(Guid.Empty, channel.AuthorId);
            Assert.NotEqual(new DateTime(), channel.CreationDate);
        }

        [Fact]
        public void MessageTest()
        {
            var message = new MessageEntity
            {
                Id = Guid.NewGuid(),
                Content = "Server",
                ChannelId = Guid.NewGuid(),
                AuthorId = Guid.NewGuid(),
                CreationDate = DateTime.UtcNow
            };

            Assert.NotNull(message);
            Assert.NotEqual(Guid.Empty, message.Id);
            Assert.NotNull(message.Content);
            Assert.NotEqual(Guid.Empty, message.ChannelId);
            Assert.NotEqual(Guid.Empty, message.AuthorId);
            Assert.NotEqual(new DateTime(), message.CreationDate);
        }

        [Fact]
        public void RoleTest()
        {
            var role = new RoleEntity
            {
                Id = Guid.NewGuid(),
                Name = "Server",
                Color = Color.Red,
                Permission = Permission.Administrator,
                ServerId = Guid.NewGuid(),
                Users = new List<Guid> {Guid.Empty},
                CreationDate = DateTime.UtcNow
            };

            Assert.NotNull(role);
            Assert.NotEqual(Guid.Empty, role.Id);
            Assert.NotNull(role.Name);
            Assert.NotEqual(Color.Black, role.Color);
            Assert.NotEqual(Permission.None, role.Permission);
            Assert.NotEqual(Guid.Empty, role.ServerId);
            Assert.NotEmpty(role.Users);
            Assert.NotEqual(new DateTime(), role.CreationDate);
        }
    }
}