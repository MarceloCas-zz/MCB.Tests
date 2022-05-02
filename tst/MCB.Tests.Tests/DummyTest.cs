using FluentAssertions;
using MCB.Core.Infra.CrossCutting.DateTime;
using MCB.Tests.Fixtures;
using MCB.Tests.Tests.DomainEntities;
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
        public void TestBase_Should_SetNewDateForDateTimeProvider()
        {
            // Arrange and Act
            GenerateNewDateForDateTimeProvider();
            var date1 = DateTimeProvider.GetDate();
            var date2 = DateTimeProvider.GetDate();
            GenerateNewDateForDateTimeProvider();
            var date3 = DateTimeProvider.GetDate();
            var date4 = DateTimeProvider.GetDate();

            // Assert
            date1.Should().Be(date2);
            date3.Should().Be(date4);
            date1.Should().NotBe(date3);
        }

        [Fact]
        public async Task TestBase_Should_Set_DateTimeProvider_GetDateCustomFunction()
        {
            // Arrange and Act
            var firstDate = DateTimeProvider.GetDate();
            await Task.Delay(500);
            var secondDate = DateTimeProvider.GetDate();

            // Assert
            firstDate.Should().Be(secondDate);
        }

        [Fact]
        public void TestBase_Should_ValidateAfterRegisterNewOperation_Success()
        {
            // Arrange
            var executionUser = FixtureBase.GenerateNewExecutionUser();
            var sourcePlatform = FixtureBase.GenerateNewSourcePlatform();

            // Act
            var dummyDomainEntity = new DummyDomainEntity().RegisterNew(
                tenantId: FixtureBase.GenerateNewTenantId(),
                executionUser,
                sourcePlatform
            );

            // Assert
            ValidateAfterRegisterNewOperation(dummyDomainEntity, executionUser, sourcePlatform);
        }

        [Fact]
        public void TestBase_Should_ValidateAfterModificationOperation_Success()
        {
            // Arrange
            var executionUser = FixtureBase.GenerateNewExecutionUser();
            var sourcePlatform = FixtureBase.GenerateNewSourcePlatform();

            GenerateNewDateForDateTimeProvider();

            var dummyDomainEntity = new DummyDomainEntity().RegisterNew(
                tenantId: FixtureBase.GenerateNewTenantId(),
                executionUser,
                sourcePlatform
            );
            var clonedDummyDomainEntity = dummyDomainEntity.DeepClone();

            GenerateNewDateForDateTimeProvider();

            // Act
            dummyDomainEntity.RegisterModification(executionUser, sourcePlatform);

            // Assert
            ValidateAfterRegisterModification(clonedDummyDomainEntity, dummyDomainEntity, executionUser, sourcePlatform);
        }
    }
}