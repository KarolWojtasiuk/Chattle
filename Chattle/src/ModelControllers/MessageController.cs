using System;
using Chattle.Database;

namespace Chattle
{
    public class MessageController
    {
        private readonly IDatabase _database;
        private readonly string _collectionName;

        public MessageController(IDatabase database, string collectionName)
        {
            _database = database;
            _collectionName = collectionName;
        }
    }
}
