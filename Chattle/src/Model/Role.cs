using System;
using System.Drawing;
using System.Collections.Generic;

namespace Chattle
{
    public class Role : IIdentifiable, IEquatable<Role>
    {
        public Guid Id { get; private set; }
        public string Name { get; internal set; }
        public string Color { get; private set; }
        public Permission Permission { get; internal set; }
        public List<Guid> Users { get; private set; }
        public DateTime CreationTime { get; private set; }

        public Role(string name, Permission permission)
        {
            Id = Guid.NewGuid();
            Name = name;
            Color = "#000000";
            Permission = permission;
            Users = new List<Guid>();
            CreationTime = DateTime.UtcNow;
        }

        public Role(string name, Permission permission, Color color)
        {
            Id = Guid.NewGuid();
            Name = name;
            Color = ColorToHexString(color);
            Permission = permission;
            Users = new List<Guid>();
            CreationTime = DateTime.UtcNow;
        }

        public static Role CreateBasicRole()
        {
            return new Role("Users", Permission.None)
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
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        public bool Equals(Role other)
        {
            return other.Id == Id;
        }
    }
}
