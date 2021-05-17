using System;
using System.Linq;
using Chattle.Database.DatabaseProviders;
using Chattle.Exceptions;
using Xunit;

namespace Chattle.Tests.Database.DatabaseProviders
{
    public class InMemoryDatabaseProviderTests
    {
        private readonly IDatabaseProvider _database = new InMemoryDatabaseProvider();

        [Fact]
        public void InsertTest()
        {
            var testObject = new TestEntity {Name = "TestObject"};

            _database.Insert(testObject, "TestCollection1");
            _database.Insert(testObject, "TestCollection2");
            _database.Insert(testObject with {Id = Guid.NewGuid()}, "TestCollection2");
            Assert.Throws<DatabaseInsertException<TestEntity>>(() => _database.Insert(testObject, "TestCollection1"));
        }

        [Fact]
        public void ReplaceTest()
        {
            var testObject = new TestEntity {Name = "TestObject"};
            var newObject = testObject with {Name = "TestObject2"};
            var differentObject = testObject with {Id = Guid.NewGuid()};

            Assert.Throws<DatabaseReplaceException<TestEntity>>(() => _database.Replace(testObject, "TestCollection1"));
            _database.Insert(testObject, "TestCollection1");
            _database.Replace(newObject, "TestCollection1");
            Assert.Throws<DatabaseReplaceException<TestEntity>>(() => _database.Replace(differentObject, "TestCollection1"));
        }

        [Fact]
        public void DeleteTest()
        {
            var testObject = new TestEntity {Name = "TestObject"};
            var differentObject = testObject with {Id = Guid.NewGuid()};

            Assert.Throws<DatabaseDeleteException<TestEntity>>(() => _database.Delete<TestEntity>(testObject.Id, "TestCollection1"));
            _database.Insert(testObject, "TestCollection1");
            _database.Delete<TestEntity>(testObject.Id, "TestCollection1");
            Assert.Throws<DatabaseDeleteException<TestEntity>>(() => _database.Delete<TestEntity>(differentObject.Id, "TestCollection1"));
        }

        [Fact]
        public void FindOneTest()
        {
            var testObject = new TestEntity {Name = "TestObject"};

            Assert.Throws<DatabaseFindException<TestEntity>>(() => _database.FindOne<TestEntity>(e => e == testObject, "TestCollection1"));
            Assert.Throws<DatabaseFindException<TestEntity>>(() => _database.FindOne<TestEntity>(e => e == testObject, "TestCollection1"));
            _database.Insert(testObject, "TestCollection1");
            Assert.Equal(testObject, _database.FindOne<TestEntity>(e => e == testObject, "TestCollection1"));
        }

        [Fact]
        public void FindManyTest()
        {
            var testObject1 = new TestEntity {Name = "TestObject"};
            var testObject2 = new TestEntity {Name = "TestObject"};

            Assert.Empty(_database.FindMany<TestEntity>(e => e.Name == "TestObject", "TestCollection1"));
            _database.Insert(testObject1, "TestCollection1");
            Assert.Single(_database.FindMany<TestEntity>(e => e.Name == "TestObject", "TestCollection1"));
            _database.Insert(testObject2, "TestCollection1");
            Assert.Equal(2, _database.FindMany<TestEntity>(e => e.Name == "TestObject", "TestCollection1").Count());
        }
    }
}