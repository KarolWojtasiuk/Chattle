using System;
using System.Linq.Expressions;
using Chattle.Models;

namespace Chattle.Exceptions
{
    public abstract class DatabaseException<T> : Exception where T : IIdentifiable
    {
        protected DatabaseException(object id, string message, Exception? innerException = null)
            : base($"{typeof(T).Name}({id}): {message}", innerException)
        {
        }
    }

    public class DatabaseInsertException<T> : DatabaseException<T> where T : IIdentifiable
    {
        public DatabaseInsertException(Guid id, string message, Exception? innerException = null) : base(id, message, innerException)
        {
        }
    }

    public class DatabaseReplaceException<T> : DatabaseException<T> where T : IIdentifiable
    {
        public DatabaseReplaceException(Guid id, string message, Exception? innerException = null) : base(id, message, innerException)
        {
        }
    }

    public class DatabaseDeleteException<T> : DatabaseException<T> where T : IIdentifiable
    {
        public DatabaseDeleteException(Guid id, string message, Exception? innerException = null) : base(id, message, innerException)
        {
        }
    }

    public class DatabaseFindException<T> : DatabaseException<T> where T : IIdentifiable
    {
        public DatabaseFindException(Expression<Func<T, bool>> expression, string message, Exception? innerException = null)
            : base(expression, message, innerException)
        {
        }
    }
}