using System;

namespace Chattle.Database.Entities
{
    public record ChannelEntity : IEntity
    {
        public Guid Id { get; private init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public Guid ServerId { get; private init; }
        public Guid AuthorId { get; private init; }
        public DateTime CreationDate { get; private init; }

        public ChannelEntity(string name, Guid serverId, Guid authorId, string description = "")
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