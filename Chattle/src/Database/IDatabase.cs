using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Chattle.Database
{
    public interface IDatabase
    {
        public void Create<T>(string collectionName, T item) where T : IIdentifiable;

        public List<T> Read<T>(string collectionName, Expression<Func<T, bool>> expression, int limit = 0) where T : IIdentifiable;

        public int Count<T>(string collectionName, Expression<Func<T, bool>> expression) where T : IIdentifiable;

        public void Update<T>(string collectionName, Guid id, string fieldName, object value) where T : IIdentifiable;

        public void Replace<T>(string collectionName, Guid id, T newItem) where T : IIdentifiable;

        public void Delete<T>(string collectionName, Guid id) where T : IIdentifiable;
    }
}
