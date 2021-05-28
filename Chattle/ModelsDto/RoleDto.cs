using System;
using System.Collections.Generic;
using Chattle.Models;

namespace Chattle.ModelsDto
{
    public record RoleDto : IDtoObject
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; init; } = String.Empty;
        public Color Color { get; init; } = Color.Black;
        public Permission Permission { get; init; } = Permission.None;
        public List<Guid> Users { get; init; } = new();
        public DateTime CreationDate { get; init; } = DateTime.UtcNow;
    }
}