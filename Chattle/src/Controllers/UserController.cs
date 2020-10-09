using System;
using Chattle.Database;

namespace Chattle
{
    public class UserController : IController
    {
        public void Create<T>(T item, IDatabase database) where T : IIdentifiable
        {
            throw new NotImplementedException();
        }

        public T Read<T>(Guid id, IDatabase database) where T : IIdentifiable
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(Guid id, IDatabase database) where T : IIdentifiable
        {
            throw new NotImplementedException();
        }
    }
}
