using System;
using System.Collections.Generic;
using Chattle.Database;

namespace Chattle
{
    public class ModelController
    {
        public IDatabase Database;
        public Dictionary<Type, IController> Controllers;

        public ModelController(IDatabase database)
        {
            Database = database;
            Controllers = new Dictionary<Type, IController>
            {
                {typeof(Account),new AccountController()}
            };
        }

        public void Create<T>(T item) where T : IIdentifiable
        {
            if (Controllers.ContainsKey(typeof(T)))
            {
                Controllers[typeof(T)].Create(item, Database);
            }
            else
            {
                throw new TypeNotSupportedException(typeof(T));
            }
        }

        public T Read<T>(Guid id) where T : IIdentifiable
        {
            if (Controllers.ContainsKey(typeof(T)))
            {
                return Controllers[typeof(T)].Read<T>(id, Database);
            }
            else
            {
                throw new TypeNotSupportedException(typeof(T));
            }
        }

        public void Delete<T>(Guid id) where T : IIdentifiable
        {
            if (Controllers.ContainsKey(typeof(T)))
            {
                Controllers[typeof(T)].Delete<T>(id, Database);
            }
            else
            {
                throw new TypeNotSupportedException(typeof(T));
            }
        }
    }
}
