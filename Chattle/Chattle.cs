using Microsoft.Extensions.DependencyInjection;

namespace Chattle
{
    public class Chattle
    {
        internal Chattle(ServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public ServiceProvider ServiceProvider { get; }
    }
}