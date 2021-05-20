using System;
using Chattle.Models;

namespace Chattle.Exceptions
{
    public class ObjectNotFoundException<T> : Exception where T : IIdentifiable
    {
        public ObjectNotFoundException(Guid id) : base($"{typeof(T)}({id}) has insufficient permissions")
        {
        }
    }
}