using System;

namespace Chattle
{
    public class Message
    {
        public Guid Id { get; private set; }
        public string Content { get; set; }
        public Guid ChannelId { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime CreationTime { get; private set; }

        public Message(string content, Guid channelId, Guid userId)
        {
            Id = Guid.NewGuid();
            Content = content;
            ChannelId = channelId;
            UserId = userId;
            CreationTime = DateTime.UtcNow;
        }
    }
}
