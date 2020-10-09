using System;
using Chattle.Database;

namespace Chattle
{
    public class ChannelController : IController
    {
        private readonly IDatabase _database;
        private readonly string _collectionName;

        public ChannelController(IDatabase database, string collectionName)
        {
            _database = database;
            _collectionName = collectionName;
        }

        public void Create<T>(T item) where T : IIdentifiable
        {
            throw new NotImplementedException();
        }

        public T Read<T>(Guid id) where T : IIdentifiable
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(Guid id) where T : IIdentifiable
        {
            throw new NotImplementedException();
        }
    }
}
