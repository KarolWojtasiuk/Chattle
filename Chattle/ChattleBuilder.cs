using Chattle.Database;
using Chattle.Database.DatabaseProviders;
using MediatR;
using Serilog;
using Microsoft.Extensions.DependencyInjection;

namespace Chattle
{
    public class ChattleBuilder
    {
        private IDatabaseProvider _databaseProvider = GetDefaultDatabaseProvider();
        private ILogger _logger = GetDefaultLogger();

        public Chattle Build()
        {
            if (_databaseProvider.GetType() == typeof(InMemoryDatabaseProvider))
            {
                _logger.Warning("You are using {Type} as database provider, this provider should only be used for testing",
                    typeof(InMemoryDatabaseProvider));
            }

            return new Chattle(InitializeServices());
        }

        public ChattleBuilder AddDatabase(IDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
            return this;
        }

        public ChattleBuilder AddLogger(ILogger logger)
        {
            _logger = logger;
            return this;
        }

        private ServiceProvider InitializeServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton(_logger);
            services.AddSingleton(_databaseProvider);
            services.AddSingleton<DatabaseController>();
            services.AddSingleton<EntityMapper>();
            services.AddMediatR(typeof(Chattle));
            return services.BuildServiceProvider();
        }

        private static ILogger GetDefaultLogger()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            return logger;
        }

        private static IDatabaseProvider GetDefaultDatabaseProvider()
        {
            return new InMemoryDatabaseProvider();
        }
    }
}