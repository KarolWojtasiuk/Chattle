using System;

namespace Chattle
{
    public class Channel : IIdentifiable, IEquatable<Channel>
    {
        public Guid Id { get; private set; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public Guid ChannelId { get; private set; }
        public DateTime CreationTime { get; private set; }

        public Channel(string name, Guid authorId, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            ChannelId = authorId;
            CreationTime = DateTime.UtcNow;
        }

        public Channel(string name, Guid authorId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = String.Empty;
            ChannelId = authorId;
            CreationTime = DateTime.UtcNow;
        }

        public bool Equals(Channel other)
        {
            return other.Id == Id;
        }
    }
}
