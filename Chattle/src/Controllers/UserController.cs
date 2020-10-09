using System;
using Chattle.Database;

namespace Chattle
{
    public class UserController : IController
    {
        private readonly IDatabase _database;
        private readonly string _collectionName;

        public UserController(IDatabase database, string collectionName)
        {
            _database = database;
            _collectionName = collectionName;
        }

        public void Create<T>(T item) where T : IIdentifiable
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(Guid id) where T : IIdentifiable
        {
            throw new NotImplementedException();
        }

        public T Read<T>(Guid id) where T : IIdentifiable
        {
            throw new NotImplementedException();
        }
    }
}
