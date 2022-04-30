using FluentAssertions;
using MCB.Core.Infra.CrossCutting.DateTime;
using MCB.Tests.Tests.Fixtures;
using MCB.Tests.Tests.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
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
        public void FixtureBase_Should_Have_DependecyInjection_Configured()
        {
            _fixture.Should().NotBeNull();
            _fixture.CreateScope().ServiceProvider.GetService<IDummyService>().Should().NotBeNull();
        }

        [Fact]
        public async Task FixtureBase_Should_Set_DateTimeProvider_GetDateCustomFunction()
        {
            // Arrang and Act
            var firstDate = DateTimeProvider.GetDate();
            await Task.Delay(500);
            var secondDate = DateTimeProvider.GetDate();

            // Assert
            firstDate.Should().Be(secondDate);
        }
    }
}