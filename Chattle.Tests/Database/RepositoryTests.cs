using System.Linq;
using Chattle.Database;
using Chattle.Database.DatabaseProviders;
using Xunit;

namespace Chattle.Tests.Database
{
    public class RepositoryTests
    {
        private readonly Repository<TestEntity> _repository = new(new InMemoryDatabaseProvider(), "TestCollection");

        [Fact]
        public void InsertTest()
        {
            var testObject = new TestEntity();

            _repository.Insert(testObject);
        }

        [Fact]
        public void ReplaceTest()
        {
            var testObject = new TestEntity();

            _repository.Replace(testObject);
            _repository.Insert(testObject);
            _repository.Replace(testObject);
        }

        [Fact]
        public void DeleteTest()
        {
            var testObject = new TestEntity();

            _repository.Delete(testObject.Id);
            _repository.Insert(testObject);
            _repository.Delete(testObject.Id);
        }

        [Fact]
        public void GetTest()
        {
            var testObject = new TestEntity();

            Assert.Null(_repository.Get(testObject.Id));
            _repository.Insert(testObject);
            Assert.Equal(testObject, _repository.Get(testObject.Id));
        }

        [Fact]
        public void FindOneTest()
        {
            var testObject = new TestEntity {Name = "TestObject"};

            Assert.Null(_repository.FindOne(e => e.Name == "TestObject"));
            _repository.Insert(testObject);
            Assert.Equal(testObject, _repository.FindOne(e => e.Name == "TestObject"));
        }

        [Fact]
        public void FindManyTest()
        {
            var testObject1 = new TestEntity {Name = "TestObject"};
            var testObject2 = new TestEntity {Name = "TestObject"};

            Assert.Empty(_repository.FindMany(e => e.Name == "TestObject"));
            _repository.Insert(testObject1);
            Assert.Single(_repository.FindMany(e => e.Name == "TestObject"));
            _repository.Insert(testObject2);
            Assert.Equal(2, _repository.FindMany(e => e.Name == "TestObject").Count());
        }

        [Fact]
        public void SafeExecuteTest()
        {
            var emptyObject = new TestEntity();
            var unsafeObject = new TestEntity {Name = null!};

            _repository.Insert(emptyObject);
            _repository.Insert(null!);
            _repository.FindOne(e => unsafeObject.Name.Length > 0);
        }
    }
}