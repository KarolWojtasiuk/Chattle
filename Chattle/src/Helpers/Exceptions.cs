using System;

namespace Chattle
{
    public class TypeNotSupportedException : Exception
    {
        public TypeNotSupportedException(Type type) : base($"ModelController does not support the `{type.Name}` type") { }
    }

    public class ModelVerificationException : Exception
    {
        public ModelVerificationException() { }

        public ModelVerificationException(string message) : base(message) { }
        public ModelVerificationException(IIdentifiable item, string message) : base($"{item.GetType().Name}({item.Id}) -> {message}") { }
    }

    public class DoesNotExistsException : ModelVerificationException
    {
        public DoesNotExistsException() : base("Object does not exists in the database.") { }
    }

    public class NotOwnerException : ModelVerificationException
    {
        public NotOwnerException(IIdentifiable item) : base(item, "Only owner can perform server-based actions.") { }
    }

    public class DuplicateException : ModelVerificationException
    {
        public DuplicateException(IIdentifiable item) : base(item, "Object with the same id already exists.") { }
    }

    public class AnotherUserAssigned : ModelVerificationException
    {
        public AnotherUserAssigned(IIdentifiable item) : base(item, "There is already a user assigned to this account.") { }
    }
}
