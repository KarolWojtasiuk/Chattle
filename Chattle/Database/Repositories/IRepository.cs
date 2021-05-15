using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Chattle.Database.Entities;

namespace Chattle.Database.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        public void Insert(T item);
        public void Replace(T item);
        public void Update<TProperty>(Guid id, Expression<Func<T, TProperty>> expression, TProperty value);
        public void Delete(Guid id);
        public T Get(Guid id);
        public T FindOne(Expression<Func<T, bool>> expression);
        public IEnumerable<T> FindMany(Expression<Func<T, bool>> expression);
    }
}