using MCB.Core.Domain.Entities.DomainEntitiesBase;
using System;

namespace MCB.Tests.Tests.DomainEntities;

public class DummyDomainEntity
    : DomainEntityBase
{
    protected override DomainEntityBase CreateInstanceForCloneInternal() => new DummyDomainEntity();

    public DummyDomainEntity RegisterNew(Guid tenantId, string executionUser, string sourcePlatform)
        => RegisterNewInternal<DummyDomainEntity>(tenantId, executionUser, sourcePlatform);
    public DummyDomainEntity RegisterModification(string executionUser, string sourcePlatform)
        => RegisterModificationInternal<DummyDomainEntity>(executionUser, sourcePlatform);

    public DummyDomainEntity DeepClone()
        => DeepCloneInternal<DummyDomainEntity>();
}
