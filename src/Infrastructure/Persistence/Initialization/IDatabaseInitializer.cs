using ARK.WebApi.Infrastructure.Multitenancy;

namespace ARK.WebApi.Infrastructure.Persistence.Initialization;

internal interface IDatabaseInitializer
{
    Task InitializeDatabasesAsync(CancellationToken cancellationToken);
    Task InitializeApplicationDbForTenantAsync(ARKTenantInfo tenant, CancellationToken cancellationToken);
}