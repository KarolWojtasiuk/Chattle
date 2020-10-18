using System;

namespace Chattle.SignalR
{
    public struct CreateResult
    {
        public Guid Id { get; set; }
    }

    public struct GetResult
    {
        public Account Account { get; set; }
    }

    public struct DeleteResult
    {
        public Guid Id { get; set; }
    }

    public struct ModifyResult<T>
    {
        public Guid Id { get; set; }
        public T NewValue { get; set; }
    }

    public struct ManageResult<T>
    {
        public Guid Id { get; set; }
        public T NewValue { get; set; }
    }

    public struct ErrorResult
    {
        public string Method { get; set; }
        public string Message { get; set; }
    }
}
