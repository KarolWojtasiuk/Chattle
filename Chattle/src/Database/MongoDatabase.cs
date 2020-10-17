using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Chattle.Database
{
    public class MongoDatabase : IDatabase
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase database;

        public MongoDatabase(string connectionString, string databaseName)
        {
            client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
        }

        public void Create<T>(string collectionName, T item) where T : IIdentifiable
        {
            database.GetCollection<T>(collectionName).InsertOne(item);
        }

        public List<T> Read<T>(string collectionName, Expression<Func<T, bool>> expression, int limit = 0) where T : IIdentifiable
        {
            var query = database.GetCollection<T>(collectionName).AsQueryable().OrderByDescending(i => i.CreationTime);
            if (limit <= 0)
            {
                return database.GetCollection<T>(collectionName).AsQueryable().Where(expression).ToList();
            }
            else
            {
                return database.GetCollection<T>(collectionName).AsQueryable().Where(expression).Take(limit).ToList();
            }
        }

        public int Count<T>(string collectionName, Expression<Func<T, bool>> expression) where T : IIdentifiable
        {
            return database.GetCollection<T>(collectionName).AsQueryable().Count(expression);
        }

        public void Update<T>(string collectionName, Guid id, string fieldName, object value) where T : IIdentifiable
        {
            database.GetCollection<T>(collectionName).UpdateOne(i => i.Id == id, Builders<T>.Update.Set(fieldName, value));
        }

        public void Replace<T>(string collectionName, Guid id, T newItem) where T : IIdentifiable
        {
            database.GetCollection<T>(collectionName).ReplaceOne(i => i.Id == id, newItem);
        }

        public void Delete<T>(string collectionName, Guid id) where T : IIdentifiable
        {
            database.GetCollection<T>(collectionName).DeleteOne(i => i.Id == id);
        }

        public void Delete<T>(string collectionName, Expression<Func<T, bool>> expression) where T : IIdentifiable
        {
            database.GetCollection<T>(collectionName).DeleteMany(expression);
        }
    }
}
