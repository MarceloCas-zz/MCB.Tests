using MCB.Core.Infra.CrossCutting.DateTime;
using MCB.Core.Infra.CrossCutting.DependencyInjection.Abstractions.Interfaces;

namespace MCB.Tests.Fixtures;

public abstract class FixtureBase
{
    // Properties
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
    private static void Initialize()
    {
        DateTimeProvider.GetDateCustomFunction = new Func<DateTimeOffset>(() => 
            new DateTimeOffset(new DateTime(2022, 01, 01, 1, 1, 1, DateTimeKind.Utc))
        );
    }

    // Abstract Methods
    protected abstract IDependencyInjectionContainer CreateDependencyInjectionContainerInternal();
    protected abstract void ConfigureDependencyInjectionContainerInternal(IDependencyInjectionContainer dependencyInjectionContainer);
    protected abstract void BuildDependencyInjectionContainerInternal(IDependencyInjectionContainer dependencyInjectionContainer);

    // Public Methods
    public IDependencyInjectionContainer CreateNewDependencyInjectionContainer()
    {
        var dependencyInjectionContainer = CreateDependencyInjectionContainerInternal();
        ConfigureDependencyInjectionContainerInternal(dependencyInjectionContainer);
        BuildDependencyInjectionContainerInternal(dependencyInjectionContainer);

        return dependencyInjectionContainer;
    }
    public static Guid GenerateNewTenantId() => Guid.NewGuid();
    public static string GenerateNewExecutionUser() => $"{nameof(ExecutionUser)} {Guid.NewGuid()}";
    public static string GenerateNewSourcePlatform() => $"{nameof(SourcePlatform)} {Guid.NewGuid()}";
}
