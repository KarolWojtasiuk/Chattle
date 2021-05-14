using System;

namespace Chattle.Database.Entities
{
    public record MessageEntity : IEntity
    {
        public Guid Id { get; private init; }
        public string Content { get; init; }
        public Guid ChannelId { get; private init; }
        public Guid AuthorId { get; private init; }
        public DateTime CreationDate { get; private init; }

        public MessageEntity(string content, Guid channelId, Guid authorId)
        {
            Id = Guid.NewGuid();
            Content = content;
            ChannelId = channelId;
            AuthorId = authorId;
            CreationDate = DateTime.UtcNow;
        }
    }
}