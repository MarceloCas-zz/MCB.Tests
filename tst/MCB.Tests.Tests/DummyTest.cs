using FluentAssertions;
using MCB.Tests.Tests.Fixtures;
using MCB.Tests.Tests.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace MCB.Tests.Tests
{
    [Collection(nameof(DefaultFixture))]
    public class DummyTest
        : TestBase
    {
        // Fields
        private readonly DefaultFixture _fixture;

        // Constructors
        public DummyTest(
            ITestOutputHelper testOutputHelper,
            DefaultFixture fixture
        ) : base(testOutputHelper)
        {
            _fixture = fixture;
        }

        [Fact]
        public void TestBase_Should_Have_TestOutputHelper_Instance()
        {
            TestOutputHelper.Should().NotBeNull();
        }

        [Fact]
        public void FixtureBase_Should_Have_DependecyIjection_Configured()
        {
            _fixture.Should().NotBeNull();
            _fixture.CreateScope().ServiceProvider.GetService<IDummyService>().Should().NotBeNull();
        }
    }
}