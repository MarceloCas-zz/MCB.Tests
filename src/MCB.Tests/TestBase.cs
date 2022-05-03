using FluentAssertions;
using MCB.Core.Domain.Entities;
using MCB.Core.Infra.CrossCutting.DateTime;
using Xunit.Abstractions;

namespace MCB.Tests
{
    public abstract class TestBase
    {
        // Fields
        private DateTimeOffset _lastDateTimeOffsetForDateTimeProvider;

        // Properties
        protected ITestOutputHelper TestOutputHelper { get; }

        // Constructors
        protected TestBase(ITestOutputHelper testOutputHelper)
        {
            TestOutputHelper = testOutputHelper;
        }

        // Protected Methods
        public void GenerateNewDateForDateTimeProvider()
        {
            _lastDateTimeOffsetForDateTimeProvider = DateTimeOffset.UtcNow;

            DateTimeProvider.GetDateCustomFunction = new Func<DateTimeOffset>(
                () => _lastDateTimeOffsetForDateTimeProvider
            );
        }
        protected static void ValidateAfterRegisterNewOperation(
            DomainEntityBase domainEntity,
            string executionUser,
            string sourcePlatform
        )
        {
            domainEntity.Id.Should().NotBe(Guid.Empty);

            domainEntity.AuditableInfo.CreatedAt.Should().Be(DateTimeProvider.GetDate());
            domainEntity.AuditableInfo.CreatedBy.Should().Be(executionUser);

            domainEntity.AuditableInfo.LastUpdatedAt.Should().BeNull();
            domainEntity.AuditableInfo.LastUpdatedBy.Should().BeNull();

            domainEntity.AuditableInfo.LastSourcePlatform.Should().Be(sourcePlatform);

            domainEntity.RegistryVersion.Should().Be(DateTimeProvider.GetDate());
        }
        protected static void ValidateAfterRegisterModification(
            DomainEntityBase domainEntityBeforeModification,
            DomainEntityBase domainEntityAfterModification,
            string executionUser,
            string sourcePlatform
        )
        {
            domainEntityAfterModification.Should().NotBeSameAs(domainEntityBeforeModification);
            domainEntityAfterModification.Id.Should().Be(domainEntityBeforeModification.Id);

            domainEntityAfterModification.AuditableInfo.Should().NotBeSameAs(domainEntityBeforeModification.AuditableInfo);

            domainEntityAfterModification.AuditableInfo.CreatedAt.Should().Be(domainEntityBeforeModification.AuditableInfo.CreatedAt);
            domainEntityAfterModification.AuditableInfo.CreatedBy.Should().Be(domainEntityBeforeModification.AuditableInfo.CreatedBy);

            domainEntityAfterModification.AuditableInfo.LastUpdatedAt.Should().BeAfter(domainEntityAfterModification.AuditableInfo.CreatedAt);
            domainEntityAfterModification.AuditableInfo.LastUpdatedAt.Should().Be(DateTimeProvider.GetDate());
            domainEntityAfterModification.AuditableInfo.LastUpdatedBy.Should().Be(executionUser);

            domainEntityAfterModification.AuditableInfo.LastSourcePlatform.Should().Be(sourcePlatform);

            domainEntityAfterModification.RegistryVersion.Should().BeAfter(domainEntityBeforeModification.RegistryVersion);
        }
    }
}
