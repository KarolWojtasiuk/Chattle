using System;

namespace Chattle
{
    public class Channel
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; private set; }

        public Channel(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            CreationTime = DateTime.UtcNow;
        }

        public Channel(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = String.Empty;
            CreationTime = DateTime.UtcNow;
        }
    }
}
