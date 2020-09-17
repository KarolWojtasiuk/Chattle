using System;
using System.Linq;
using Xunit;
using Chattle.Database;

namespace Chattle.Tests
{
    public class DatabaseTests
    {
        public const string ConnectionString = "mongodb+srv://testUser:testPassword@cluster-ktbsc.azure.mongodb.net";
        public const string DatabaseName = "TestDatabase";
        public const string CollectionName = "MongoTest";

        [Fact]
        public void MongoTest()
        {
            var database = new MongoDatabase(ConnectionString, DatabaseName);

            var account = new Account("testAccount");

            Assert.Equal(0, database.Count<Account>(CollectionName, x => true));
            Assert.Empty(database.Read<Account>(CollectionName, x => true));

            database.Create(CollectionName, account);

            Assert.Equal(1, database.Count<Account>(CollectionName, x => true));
            Assert.Equal(0, database.Count<Account>(CollectionName, x => x.Id == Guid.Empty));

            var remoteAccount = database.Read<Account>(CollectionName, x => x.Id == account.Id, 1).First();
            Assert.Equal(account, remoteAccount);

            account.Username = "testAccount2";
            database.Update(CollectionName, account.Id, account);
            Assert.Equal(account, remoteAccount);

            database.Delete<Account>(CollectionName, account.Id);
            Assert.Equal(0, database.Count<Account>(CollectionName, x => true));
            Assert.Empty(database.Read<Account>(CollectionName, x => true));
        }
        //
    }
}
