using System;

namespace Chattle.Database.Entities
{
    public record MessageEntity : IEntity
    {
        public Guid Id { get; init; } = Guid.Empty;
        public string Content { get; init; } = String.Empty;
        public Guid ChannelId { get; init; } = Guid.Empty;
        public Guid AuthorId { get; init; } = Guid.Empty;
        public DateTime CreationDate { get; init; } = DateTime.MinValue;
    }
}