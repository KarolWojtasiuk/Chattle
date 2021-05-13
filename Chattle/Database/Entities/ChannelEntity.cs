using System;

namespace Chattle.Database.Entities
{
    public record ChannelEntity
    {
        public Guid Id { get; private init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public Guid ServerId { get; private init; }
        public Guid AuthorId { get; private init; }
        public DateTime CreationDate { get; private init; }

        public ChannelEntity(string name, string description, Guid serverId, Guid authorId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            ServerId = serverId;
            AuthorId = authorId;
            CreationDate = DateTime.UtcNow;
        }
    }
}