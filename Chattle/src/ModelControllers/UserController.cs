using System;
using Chattle.Database;

namespace Chattle
{
    public class UserController
    {
        private readonly IDatabase _database;
        private readonly string _collectionName;

        public UserController(IDatabase database, string collectionName)
        {
            _database = database;
            _collectionName = collectionName;
        }
    }
}
