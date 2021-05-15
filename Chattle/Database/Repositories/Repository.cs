using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Chattle.Database.Entities;

namespace Chattle.Database.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : IEntity
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly string _collectionName;

        protected Repository(IDatabaseContext databaseContext, string collectionName)
        {
            _databaseContext = databaseContext;
            _collectionName = collectionName;
        }

        public void Insert(T item)
        {
            _databaseContext.Insert(item, _collectionName);
        }

        public void Replace(T item)
        {
            _databaseContext.Replace(item, _collectionName);
        }

        public void Update<TProperty>(Guid id, Expression<Func<T, TProperty>> expression, TProperty value)
        {
            _databaseContext.Update(id, expression, value, _collectionName);
        }

        public void Delete(Guid id)
        {
            _databaseContext.Delete<T>(id, _collectionName);
        }

        public T Get(Guid id)
        {
            return _databaseContext.FindOne<T>(e => e.Id == id, _collectionName);
        }

        public T FindOne(Expression<Func<T, bool>> expression)
        {
            return _databaseContext.FindOne(expression, _collectionName);
        }

        public IEnumerable<T> FindMany(Expression<Func<T, bool>> expression)
        {
            return _databaseContext.FindMany(expression, _collectionName);
        }
    }
}