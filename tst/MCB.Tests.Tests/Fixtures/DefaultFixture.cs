using MCB.Tests.Fixtures;
using MCB.Tests.Tests.Services;
using MCB.Tests.Tests.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MCB.Tests.Tests.Fixtures
{
    [CollectionDefinition(nameof(DefaultFixture))]
    public class DefaultFixtureCollection
        : ICollectionFixture<DefaultFixture>
    {

    }
    public class DefaultFixture
        : FixtureBase
    {
        protected override void ConfigureServices(ServiceCollection services)
        {
            services.AddScoped<IDummyService, DummyService>();
        }
    }
}
