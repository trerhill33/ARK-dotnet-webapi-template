using Finbuckle.MultiTenant.Stores;
using ARK.WebApi.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ARK.WebApi.Infrastructure.Multitenancy;

public class TenantDbContext : EFCoreStoreDbContext<ARKTenantInfo>
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ARKTenantInfo>().ToTable("Tenants", SchemaNames.MultiTenancy);
    }
}