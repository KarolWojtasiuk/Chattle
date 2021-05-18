using System;
using System.Collections.Generic;
using Chattle.Models;

namespace Chattle.Database.Entities
{
    public record RoleEntity : IEntity
    {
        public Guid Id { get; init; } = Guid.Empty;
        public string Name { get; init; } = String.Empty;
        public Color Color { get; init; } = Color.Black;
        public Permission Permission { get; init; } = Permission.None;
        public Guid ServerId { get; init; } = Guid.Empty;
        public IEnumerable<Guid> Users { get; init; } = Array.Empty<Guid>();
        public DateTime CreationDate { get; init; } = DateTime.MinValue;
    }
}