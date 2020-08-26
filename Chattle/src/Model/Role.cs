using System;
using System.Drawing;
using System.Collections.Generic;

namespace Chattle
{
    public class Role
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public List<Guid> Users { get; private set; }

        public Role(string name, Color color)
        {
            Id = Guid.NewGuid();
            Name = name;
            Color = ColorToHexString(color);
            Users = new List<Guid>();
        }

        private static string ColorToHexString(Color color)
        {
            return $"#{color.R:2}{color.G:2}{color.B:2}";
        }
    }
}
