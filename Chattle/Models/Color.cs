namespace Chattle.Models
{
    public readonly struct Color
    {
        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public byte R { get; }
        public byte G { get; }
        public byte B { get; }

        public static readonly Color Black = new(0, 0, 0);
        public static readonly Color White = new(255, 255, 255);
        public static readonly Color Red = new(255, 0, 0);
        public static readonly Color Green = new(0, 255, 0);
        public static readonly Color Blue = new(0, 0, 255);

        public string ToHexString()
        {
            return $"#{R:X2}{G:X2}{B:X2}";
        }
    }
}