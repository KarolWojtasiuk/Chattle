using System;
using Chattle.Database.Entities;

namespace Chattle.Tests.Database
{
    public record TestEntity : IEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; init; } = String.Empty;
        public DateTime CreationDate { get; init; } = DateTime.UtcNow;
    }
}