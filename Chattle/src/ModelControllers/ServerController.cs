using System;
using Chattle.Database;

namespace Chattle
{
    public class ServerController
    {
        private readonly IDatabase _database;
        private readonly string _collectionName;

        public ServerController(IDatabase database, string collectionName)
        {
            _database = database;
            _collectionName = collectionName;
        }
    }
}
