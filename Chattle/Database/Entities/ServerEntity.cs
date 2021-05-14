using System;

namespace Chattle.Database.Entities
{
    public record ServerEntity : IEntity
    {
        public Guid Id { get; init; } = Guid.Empty;
        public string Name { get; init; } = String.Empty;
        public string Description { get; init; } = String.Empty;
        public Uri? ImageUri { get; init; } = null;
        public Guid OwnerId { get; init; } = Guid.Empty;
        public DateTime CreationDate { get; init; } = DateTime.MinValue;
    }
}