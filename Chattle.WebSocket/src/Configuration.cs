using Microsoft.Extensions.Configuration;

namespace Chattle.WebSocket
{
    public class Configuration
    {
        public string Address { get; set; }

        internal static Configuration PrepareConfiguration(string filename)
        {
            var configuration = new Configuration();

            new ConfigurationBuilder().AddJsonFile(filename, false, true).Build().Bind(configuration);

            return configuration;
        }
    }
}
