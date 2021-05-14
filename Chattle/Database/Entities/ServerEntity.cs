using System;

namespace Chattle.Database.Entities
{
    public record ServerEntity
    {
        public Guid Id { get; private init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public Uri? ImageUri { get; init; }
        public Guid OwnerId { get; init; }
        public DateTime CreationDate { get; private init; }

        public ServerEntity(string name, string description, Guid ownerId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            OwnerId = ownerId;
            CreationDate = DateTime.UtcNow;
        }
    }
}