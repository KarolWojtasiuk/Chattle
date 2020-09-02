using System;
using System.Drawing;
using System.Collections.Generic;

namespace Chattle
{
    public class Role : IIdentifiable
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Color { get; private set; }
        public List<Guid> Users { get; private set; }
        public DateTime CreationTime { get; private set; }

        public Role(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Color = "#000000";
            Users = new List<Guid>();
            CreationTime = DateTime.UtcNow;
        }

        public Role(string name, Color color)
        {
            Id = Guid.NewGuid();
            Name = name;
            Color = ColorToHexString(color);
            Users = new List<Guid>();
            CreationTime = DateTime.UtcNow;
        }

        public static Role CreateBasicRole()
        {
            return new Role("Users")
            {
                Id = Guid.Empty,
                Name = "Users",
                Color = "#000000",
                Users = new List<Guid>(),
                CreationTime = DateTime.UtcNow
            };
        }

        public void ChangeColor(Color color)
        {
            Color = ColorToHexString(color);
        }

        private static string ColorToHexString(Color color)
        {
            return $"#{color.R:2}{color.G:2}{color.B:2}";
        }
    }
}
