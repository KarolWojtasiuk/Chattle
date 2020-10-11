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

    public class DoesNotExistException<T> : ModelVerificationException<T> where T : IIdentifiable
    {
        public DoesNotExistException(Guid id) : base(id, "Object does not exist.") { }
    }

    public class AlreadyExistsException<T> : ModelVerificationException<T> where T : IIdentifiable
    {
        public AlreadyExistsException(Guid id) : base(id, "Object does not exist.") { }
    }
}
