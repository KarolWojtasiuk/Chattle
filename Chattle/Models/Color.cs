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

        public byte R { get; init; }
        public byte G { get; init; }
        public byte B { get; init; }

        public static readonly Color Black = new Color(0, 0, 0);
        public static readonly Color White = new Color(255, 255, 255);
        public static readonly Color Red = new Color(255, 0, 0);
        public static readonly Color Green = new Color(0, 255, 0);
        public static readonly Color Blue = new Color(0, 0, 255);

        public string ToHexString()
        {
            return $"#{R:X2}{G:X2}{B:X2}";
        }
    }
}