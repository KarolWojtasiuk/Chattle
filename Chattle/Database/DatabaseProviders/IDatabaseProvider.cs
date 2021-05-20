using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Chattle.Models;

namespace Chattle.Database.DatabaseProviders
{
    public interface IDatabaseProvider
    {
        public void Insert<T>(T item, string collectionName) where T : IIdentifiable;

        public void Replace<T>(T item, string collectionName) where T : IIdentifiable;

        public void Delete<T>(Guid id, string collectionName) where T : IIdentifiable;

        public T FindOne<T>(Expression<Func<T, bool>> expression, string collectionName) where T : IIdentifiable;

        public IEnumerable<T> FindMany<T>(Expression<Func<T, bool>> expression, string collectionName) where T : IIdentifiable;
    }
}