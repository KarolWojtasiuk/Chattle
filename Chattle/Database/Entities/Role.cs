using System;
using System.Collections.Generic;
using System.Linq;
using Chattle.Models;

namespace Chattle.Database.Entities
{
    public record RoleEntity : IEntity
    {
        public Guid Id { get; private init; }

        public string Name { get; init; }

        //TODO: Add Color implementation
        // public Color Color { get; init; }
        public Permission Permission { get; init; }
        public Guid ServerId { get; private init; }
        private IReadOnlyList<Guid> Users { get; init; }
        public DateTime CreationDate { get; private init; }

        public RoleEntity(string name /*, Color color */, Permission permission, Guid serverId, IEnumerable<Guid> users)
        {
            Id = Guid.NewGuid();
            Name = name;
            // Color = color;
            Permission = permission;
            ServerId = serverId;
            Users = users.ToList();
            CreationDate = DateTime.UtcNow;
        }
    }
}