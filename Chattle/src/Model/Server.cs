using System;
using System.Collections.Generic;

namespace Chattle
{
    public class Server : IIdentifiable, IEquatable<Server>
    {
        public Guid Id { get; private set; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public Guid OwnerId { get; private set; }
        public Uri Image { get; internal set; }
        public List<Role> Roles { get; private set; }
        public DateTime CreationTime { get; private set; }

        public Server(string name, Guid authorId, string description, Uri image)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            OwnerId = authorId;
            Image = image;
            Roles = new List<Role> { Role.CreateBasicRole() };
            CreationTime = DateTime.UtcNow;
        }

        public Server(string name, Guid authorId, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            OwnerId = authorId;
            Image = GetDefaultServerImage();
            Roles = new List<Role> { Role.CreateBasicRole() };
            CreationTime = DateTime.UtcNow;
        }

        public Server(string name, Guid authorId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = String.Empty;
            OwnerId = authorId;
            Image = GetDefaultServerImage();
            Roles = new List<Role> { Role.CreateBasicRole() };
            CreationTime = DateTime.UtcNow;
        }

        private Uri GetDefaultServerImage() => DefaultImage.GetServerImage(Id);

        public bool Equals(Server other)
        {
            return other.Id == Id;
        }
    }
}
