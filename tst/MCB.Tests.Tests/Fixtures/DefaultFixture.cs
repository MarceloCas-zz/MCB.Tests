using MCB.Core.Infra.CrossCutting.DependencyInjection;
using MCB.Core.Infra.CrossCutting.DependencyInjection.Abstractions.Interfaces;
using MCB.Tests.Fixtures;
using MCB.Tests.Tests.Services;
using MCB.Tests.Tests.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MCB.Tests.Tests.Fixtures;

[CollectionDefinition(nameof(DefaultFixture))]
public class DefaultFixtureCollection
    : ICollectionFixture<DefaultFixture>
{

}
public class DefaultFixture
    : FixtureBase
{
    // Fields
    private IServiceCollection _serviceCollection;

    // Protected Methods
    protected override IDependencyInjectionContainer CreateDependencyInjectionContainerInternal()
    {
        return new DependencyInjectionContainer(new ServiceCollection());
    }
    protected override void BuildDependencyInjectionContainerInternal(IDependencyInjectionContainer dependencyInjectionContainer)
    {
        ((DependencyInjectionContainer)dependencyInjectionContainer).Build(_serviceCollection.BuildServiceProvider());
    }
    protected override void ConfigureDependencyInjectionContainerInternal(IDependencyInjectionContainer dependencyInjectionContainer)
    {
        dependencyInjectionContainer.RegisterScoped<IDummyService, DummyService>();
    }

}
