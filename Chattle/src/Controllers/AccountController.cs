using System;
using System.Linq;
using Chattle.Database;

namespace Chattle
{
    public class AccountController : IController
    {
        private readonly IDatabase _database;
        private readonly string _collectionName;

        public AccountController(IDatabase database, string collectionName)
        {
            _database = database;
            _collectionName = collectionName;
        }

        public void Create<T>(T item) where T : IIdentifiable
        {
            _database.Create(_collectionName, item);
        }

        public T Read<T>(Guid id) where T : IIdentifiable
        {
            return _database.Read<T>(_collectionName, a => a.Id == id, 1).First();
        }

        public void Delete<T>(Guid id) where T : IIdentifiable
        {
            _database.Delete<T>(_collectionName, id);
        }

        public void SetActive(Guid id, bool isActive)
        {
            _database.Update<Account>(_collectionName, id, "IsActive", isActive);
        }

        public void SetUsername(Guid id, string username)
        {
            _database.Update<Account>(_collectionName, id, "Username", username);
        }
    }
}
