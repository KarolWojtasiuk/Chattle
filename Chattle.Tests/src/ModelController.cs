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
        public void AccountTest()
        {
            var account = new Account("testAccount");
            account.ChangePassword("testPassword");

            var modelController = new ModelController(Database1);
            modelController.Databases.Add(Database2);

            modelController.CreateAccount(account);
            Assert.Equal(account, Database1.Read<Account>("Accounts", a => a.Id == account.Id).FirstOrDefault());
            Assert.Equal(account, Database2.Read<Account>("Accounts", a => a.Id == account.Id).FirstOrDefault());
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
            Assert.Null(Database1.Read<Account>("Accounts", a => a.Id == account.Id).FirstOrDefault());
            Assert.Null(Database2.Read<Account>("Accounts", a => a.Id == account.Id).FirstOrDefault());
        }
    }
}
