using Microsoft.Extensions.DependencyInjection;

namespace MCB.Tests.Fixtures
{
    public abstract class FixtureBase
    {
        // Fields
        private IServiceProvider _serviceProvider;

        // Constructors
        protected FixtureBase()
        {
            Initialize();
        }

        // Private Methods
        private void Initialize()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        // Abstract Methods
        protected abstract void ConfigureServices(ServiceCollection services);

        // Public Methods
        public IServiceScope CreateScope()
        {
            return _serviceProvider.CreateScope();
        }
    }
}
