using System;
using System.Linq.Expressions;
using Chattle.Database.Entities;

namespace Chattle.Exceptions
{
    public abstract class DatabaseException<T> : Exception where T : IEntity
    {
        protected DatabaseException(object id, string message, Exception? innerException = null)
            : base($"{typeof(T).Name}({id}): {message}", innerException)
        {
        }
    }

    public class DatabaseInsertException<T> : DatabaseException<T> where T : IEntity
    {
        public DatabaseInsertException(Guid id, string message, Exception? innerException = null) : base(id, message, innerException)
        {
        }
    }

    public class DatabaseReplaceException<T> : DatabaseException<T> where T : IEntity
    {
        public DatabaseReplaceException(Guid id, string message, Exception? innerException = null) : base(id, message, innerException)
        {
        }
    }

    public class DatabaseDeleteException<T> : DatabaseException<T> where T : IEntity
    {
        public DatabaseDeleteException(Guid id, string message, Exception? innerException = null) : base(id, message, innerException)
        {
        }
    }

    public class DatabaseFindException<T> : DatabaseException<T> where T : IEntity
    {
        public DatabaseFindException(Expression<Func<T, bool>> expression, string message, Exception? innerException = null)
            : base(expression, message, innerException)
        {
        }
    }
}