using System;

namespace Chattle.Database.Entities
{
    public record ChannelEntity : IEntity
    {
        public Guid Id { get; init; } = Guid.Empty;
        public string Name { get; init; } = String.Empty;
        public string Description { get; init; } = String.Empty;
        public Guid ServerId { get; init; } = Guid.Empty;
        public Guid AuthorId { get; init; } = Guid.Empty;
        public DateTime CreationDate { get; init; } = DateTime.MinValue;
    }
}