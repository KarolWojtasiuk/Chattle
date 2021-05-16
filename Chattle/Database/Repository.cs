using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Chattle.Database.DatabaseProviders;
using Chattle.Database.Entities;

namespace Chattle.Database
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        public Repository(IDatabaseProvider databaseProvider, string collectionName)
        {
            _databaseProvider = databaseProvider;
            _collectionName = collectionName;
        }

        private readonly IDatabaseProvider _databaseProvider;
        private readonly string _collectionName;

        public void Insert(T item)
        {
            _databaseProvider.Insert(item, _collectionName);
        }

        public void Replace(T item)
        {
            _databaseProvider.Replace(item, _collectionName);
        }

        public void Delete(Guid id)
        {
            _databaseProvider.Delete<T>(id, _collectionName);
        }

        public T Get(Guid id)
        {
            return _databaseProvider.FindOne<T>(e => e.Id == id, _collectionName);
        }

        public T FindOne(Expression<Func<T, bool>> expression)
        {
            return _databaseProvider.FindOne(expression, _collectionName);
        }

        public IEnumerable<T> FindMany(Expression<Func<T, bool>> expression)
        {
            return _databaseProvider.FindMany(expression, _collectionName);
        }
    }
}