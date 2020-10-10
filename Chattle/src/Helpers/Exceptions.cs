using System;

namespace Chattle
{
    public class ModelVerificationException<T> : Exception where T : IIdentifiable
    {
        public ModelVerificationException(Guid id, string message) : base($"{typeof(T)}({id}) --> {message}") { }
    }

    public class InsufficientPermissionsException<T> : ModelVerificationException<T> where T : IIdentifiable
    {
        public InsufficientPermissionsException(Guid id) : base(id, "Object has insufficient permissions.") { }
    }

    public class DoesNotExist<T> : ModelVerificationException<T> where T : IIdentifiable
    {
        public DoesNotExist(Guid id) : base(id, "Object does not exist.") { }
    }
}
