using System;
using Chattle.Database;

namespace Chattle
{
    public interface IController
    {
        public void Create<T>(T item) where T : IIdentifiable;
        public T Read<T>(Guid id) where T : IIdentifiable;
        public void Delete<T>(Guid id) where T : IIdentifiable;
    }
}
