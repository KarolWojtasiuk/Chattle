using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Chattle.Database.Entities;
using Chattle.Exceptions;

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
                if (_database[collectionName].FirstOrDefault(x => x.Id == item.Id) is not null)
                {
                    throw new DatabaseInsertException<T>(item.Id, "Entity already exists.");
                }

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
                    throw new DatabaseReplaceException<T>(item.Id, "Entity does not exist.");
                }

                _database[collectionName][index] = item;
                return;
            }

            _database.Add(collectionName, new List<IEntity>());
            throw new DatabaseReplaceException<T>(item.Id, "Entity does not exist.");
        }

        public void Delete<T>(Guid id, string collectionName) where T : IEntity
        {
            if (_database.ContainsKey(collectionName))
            {
                var removedObjects = _database[collectionName].RemoveAll(x => x.Id == id);

                if (removedObjects == 0)
                {
                    throw new DatabaseDeleteException<T>(id, "Entity does not exist.");
                }

                return;
            }

            _database.Add(collectionName, new List<IEntity>());
            throw new DatabaseDeleteException<T>(id, "Entity does not exist.");
        }

        public T FindOne<T>(Expression<Func<T, bool>> expression, string collectionName) where T : IEntity
        {
            if (_database.ContainsKey(collectionName))
            {
                var entity = _database[collectionName].Cast<T>().FirstOrDefault(expression.Compile());
                if (entity is null)
                {
                    throw new DatabaseFindException<T>(expression, "Entity does not exist.");
                }

                return entity;
            }

            _database.Add(collectionName, new List<IEntity>());
            throw new DatabaseFindException<T>(expression, "Entity does not exist.");
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