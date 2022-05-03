using MCB.Core.Infra.CrossCutting.DateTime;
using Microsoft.Extensions.DependencyInjection;

namespace MCB.Tests.Fixtures
{
    public abstract class FixtureBase
    {
        // Fields
        private IServiceProvider _serviceProvider;

        public Guid TenantId { get; }
        public string ExecutionUser { get; }
        public string SourcePlatform { get; }

        // Constructors
        protected FixtureBase()
        {
            Initialize();

            TenantId = GenerateNewTenantId();
            ExecutionUser = GenerateNewExecutionUser();
            SourcePlatform = GenerateNewSourcePlatform();
        }

        // Private Methods
        private void Initialize()
        {
            DateTimeProvider.GetDateCustomFunction = new Func<DateTimeOffset>(() => 
                new DateTimeOffset(new DateTime(2022, 01, 01, 1, 1, 1, DateTimeKind.Utc))
            );

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        // Abstract Methods
        protected abstract void ConfigureServices(ServiceCollection services);

        // Public Methods
        public static Guid GenerateNewTenantId() => Guid.NewGuid();
        public static string GenerateNewExecutionUser() => $"{nameof(ExecutionUser)} {Guid.NewGuid()}";
        public static string GenerateNewSourcePlatform() => $"{nameof(SourcePlatform)} {Guid.NewGuid()}";

        public IServiceScope CreateScope()
        {
            return _serviceProvider.CreateScope();
        }
    }
}
