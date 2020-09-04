using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using MongoDB.Driver;

namespace Chattle.Database
{
    public class MongoDatabase : IDatabaseProvider
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase database;

        public MongoDatabase(string databaseName)
        {
            client = new MongoClient();
            database = client.GetDatabase(databaseName);
        }

        public void Create<T>(string collectionName, T item) where T : IIdentifiable
        {
            database.GetCollection<T>(collectionName).InsertOne(item);
        }

        public List<T> Read<T>(string collectionName, Expression<Func<T, bool>> expression, int limit = 0) where T : IIdentifiable
        {
            if (limit <= 0)
            {
                return database.GetCollection<T>(collectionName).AsQueryable().Where(expression).ToList();
            }
            else
            {
                return database.GetCollection<T>(collectionName).AsQueryable().Where(expression).Take(limit).ToList();
            }
        }

        public void Update<T>(string collectionName, Guid id, T newItem) where T : IIdentifiable
        {
            database.GetCollection<T>(collectionName).ReplaceOne(i => i.Id == id, newItem);
        }

        public void Delete<T>(string collectionName, Guid id) where T : IIdentifiable
        {
            database.GetCollection<T>(collectionName).DeleteOne(i => i.Id == id);
        }
    }
}
