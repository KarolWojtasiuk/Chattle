using System;

namespace Chattle
{
    public class InsufficientPermissionsException : Exception
    {
        public InsufficientPermissionsException(Guid id) : base($"User({id}) has insufficient permissions.") { }
    }

    public class ModelVerificationException<T> : Exception where T : IIdentifiable
    {
        public ModelVerificationException(Guid id, string message) : base($"{typeof(T)}({id}) --> {message}") { }
    }
}
