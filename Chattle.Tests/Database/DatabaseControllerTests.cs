using Chattle.Database;
using Chattle.Database.DatabaseProviders;
using Xunit;

namespace Chattle.Tests.Database
{
    public class DatabaseControllerTests
    {
        [Fact]
        public void RepositoriesTest()
        {
            var databaseController = new DatabaseController(new InMemoryDatabaseProvider());

            Assert.NotNull(databaseController.Accounts);
            Assert.NotNull(databaseController.Users);
            Assert.NotNull(databaseController.Servers);
            Assert.NotNull(databaseController.Channels);
            Assert.NotNull(databaseController.Messages);
            Assert.NotNull(databaseController.Roles);
        }
    }
}