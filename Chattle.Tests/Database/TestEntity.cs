using System;
using Chattle.Models;

namespace Chattle.Tests.Database
{
    public record TestEntity : IIdentifiable
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; init; } = String.Empty;
        public DateTime CreationDate { get; init; } = DateTime.UtcNow;
    }
}