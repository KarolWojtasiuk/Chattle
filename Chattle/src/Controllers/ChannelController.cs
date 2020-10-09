using System;
using Chattle.Database;

namespace Chattle
{
    public class ChannelController
    {
        private readonly IDatabase _database;
        private readonly string _collectionName;

        public ChannelController(IDatabase database, string collectionName)
        {
            _database = database;
            _collectionName = collectionName;
        }
    }
}
