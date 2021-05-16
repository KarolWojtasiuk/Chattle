using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Chattle.Database.Entities;

namespace Chattle.Database.DatabaseProviders
{
    public class InMemoryDatabaseProvider : IDatabaseProvider
    {
        public InMemoryDatabaseProvider()
        {
            _database = new Dictionary<string, List<IEntity>>();
        }

        private readonly Dictionary<string, List<IEntity>> _database;

        public void Insert<T>(T item, string collectionName) where T : IEntity
        {
            if (_database.ContainsKey(collectionName))
            {
                _database[collectionName].Add(item);
                return;
            }

            _database.Add(collectionName, new List<IEntity> {item});
        }

        public void Replace<T>(T item, string collectionName) where T : IEntity
        {
            if (_database.ContainsKey(collectionName))
            {
                var index = _database[collectionName].FindIndex(e => e.Id == item.Id);
                if (index == -1)
                {
                    //TODO: Implement case where the original object does not exist when calling Replace();
                }

                _database[collectionName][index] = item;
                return;
            }

            _database.Add(collectionName, new List<IEntity>());
            //TODO: Implement case where the original object does not exist when calling Replace();
        }

        public void Delete<T>(Guid id, string collectionName) where T : IEntity
        {
            if (_database.ContainsKey(collectionName))
            {
                var removedObjects = _database[collectionName].RemoveAll(x => x.Id == id);

                if (removedObjects == 0)
                {
                    //TODO: Implement case where the original object does not exist when calling Delete();
                }

                return;
            }

            _database.Add(collectionName, new List<IEntity>());
            //TODO: Implement case where the original object does not exist when calling Delete();
        }

        public T FindOne<T>(Expression<Func<T, bool>> expression, string collectionName) where T : IEntity
        {
            if (_database.ContainsKey(collectionName))
            {
                var entity = _database[collectionName].Cast<T>().FirstOrDefault(expression.Compile());
                if (entity is null)
                {
                    //TODO: Implement case where the original object does not exist when calling FindOne();
                }

                return entity;
            }

            _database.Add(collectionName, new List<IEntity>());
            return default;
            //TODO: Implement case where the original object does not exist when calling FindOne();
        }

        public IEnumerable<T> FindMany<T>(Expression<Func<T, bool>> expression, string collectionName) where T : IEntity
        {
            if (_database.ContainsKey(collectionName))
            {
                var entities = _database[collectionName].Cast<T>().Where(expression.Compile());

                return entities;
            }

            _database.Add(collectionName, new List<IEntity>());
            return Array.Empty<T>();
        }
    }
}