using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Chattle.Database.Entities;

namespace Chattle.Database
{
    public interface IDatabaseContext
    {
        public void Insert<T>(T item, string collectionName) where T : IEntity;

        public void Replace<T>(T item, string collectionName) where T : IEntity;

        public void Update<TEntity, TProperty>(Guid id, Expression<Func<TEntity, TProperty>> expression, TProperty value, string collectionName)
            where TEntity : IEntity;

        public void Delete<T>(Guid id, string collectionName) where T : IEntity;

        public T FindOne<T>(Expression<Func<T, bool>> expression, string collectionName) where T : IEntity;

        public IEnumerable<T> FindMany<T>(Expression<Func<T, bool>> expression, string collectionName) where T : IEntity;
    }
}