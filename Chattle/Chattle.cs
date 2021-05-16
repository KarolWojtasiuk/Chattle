using Chattle.Database;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Chattle
{
    public class Chattle
    {
        public Chattle(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            InitializeServices();
        }

        private readonly IDatabaseContext _databaseContext;

        private void InitializeServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton(_databaseContext);
            services.AddSingleton<DatabaseController>();
            services.AddMediatR(typeof(Chattle));
            services.BuildServiceProvider();
        }
    }
}