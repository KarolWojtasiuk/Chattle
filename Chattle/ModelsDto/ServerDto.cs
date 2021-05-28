using System;
using System.Collections.Generic;

namespace Chattle.ModelsDto
{
    public record ServerDto : IDtoObject
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; init; } = String.Empty;
        public string Description { get; init; } = String.Empty;
        public Uri? ImageUri { get; init; } = null;
        public Guid OwnerId { get; init; } = Guid.Empty;
        public List<RoleDto> Roles { get; init; } = new();
        public DateTime CreationDate { get; init; } = DateTime.UtcNow;
    }
}