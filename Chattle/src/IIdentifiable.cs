using System;

namespace Chattle
{
    public interface IIdentifiable
    {
        public Guid Id { get; }
        public DateTime CreationTime { get; }
    }
}
