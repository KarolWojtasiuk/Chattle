using System;

namespace Chattle.Database.Entities
{
    public interface IEntity
    {
        public Guid Id { get; }
        public DateTime CreationDate { get; }
    }
}