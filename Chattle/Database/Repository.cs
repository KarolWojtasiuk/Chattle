using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Chattle.Database.DatabaseProviders;
using Chattle.Exceptions;
using Chattle.Models;
using Serilog;

namespace Chattle.Database
{
    public class Repository<T> : IRepository<T> where T : IIdentifiable
    {
        public Repository(IDatabaseProvider databaseProvider, string collectionName, ILogger? logger = null)
        {
            _databaseProvider = databaseProvider;
            _collectionName = collectionName;
            _logger = logger;
        }

        private readonly IDatabaseProvider _databaseProvider;
        private readonly string _collectionName;
        private readonly ILogger? _logger;

        public void Insert(T item)
        {
            SafeExecute(() => _databaseProvider.Insert(item, _collectionName));
        }

        public void Replace(T item)
        {
            SafeExecute(() => _databaseProvider.Replace(item, _collectionName));
        }

        public void Delete(Guid id)
        {
            SafeExecute(() => _databaseProvider.Delete<T>(id, _collectionName));
        }

        public T? Get(Guid id)
        {
            return SafeExecute(() => _databaseProvider.FindOne<T>(e => e.Id == id, _collectionName));
        }

        public T? FindOne(Expression<Func<T, bool>> expression)
        {
            return SafeExecute(() => _databaseProvider.FindOne(expression, _collectionName));
        }

        public IEnumerable<T> FindMany(Expression<Func<T, bool>> expression)
        {
            return SafeExecute(() => _databaseProvider.FindMany(expression, _collectionName)) ?? Array.Empty<T>();
        }

        private TResult? SafeExecute<TResult>(Func<TResult> func)
        {
            try
            {
                return func();
            }
            catch (DatabaseException<T> e)
            {
                _logger?.Warning(e, "Repository<{Type}>({CollectionName}) encountered a database exception", typeof(T), _collectionName);
            }
            catch (Exception e)
            {
                _logger?.Error(e, "Repository<{Type}>({CollectionName}) encountered an unexpected exception", typeof(T), _collectionName);
            }

            return default;
        }

        private void SafeExecute(Action action)
        {
            try
            {
                action();
            }
            catch (DatabaseException<T> e)
            {
                _logger?.Warning(e, "Repository<{Type}>({CollectionName}) encountered a database exception", typeof(T), _collectionName);
            }
            catch (Exception e)
            {
                _logger?.Error(e, "Repository<{Type}>({CollectionName}) encountered an unexpected exception", typeof(T), _collectionName);
            }
        }
    }
}