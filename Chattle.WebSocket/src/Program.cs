namespace Chattle.WebSocket
{
    public static class Program
    {
        public static Configuration Configuration;

        private static void Main()
        {
            Configuration = Configuration.PrepareConfiguration("appsettings.json");
        }
    }
}
