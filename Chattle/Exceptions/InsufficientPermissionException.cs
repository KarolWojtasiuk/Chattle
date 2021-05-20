using System;
using Chattle.Models;

namespace Chattle.Exceptions
{
    public class InsufficientPermissionException<T> : Exception where T : IIdentifiable
    {
        public InsufficientPermissionException(Guid id) : base($"{typeof(T)}({id}) has insufficient permissions")
        {
        }
    }
}