using ARK.WebApi.Application.Common.Events;
using ARK.WebApi.Application.Common.Interfaces;
using ARK.WebApi.Domain.Catalog;
using ARK.WebApi.Domain.Support;
using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ARK.WebApi.Infrastructure.Persistence.Context;

public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUser currentUser, ISerializerService serializer, IOptions<DatabaseSettings> dbSettings, IEventPublisher events)
        : base(currentTenant, options, currentUser, serializer, dbSettings, events)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<SupportTicket> SupportTickets => Set<SupportTicket>();
    public DbSet<SupportTicketCategory> SupportTicketCategories => Set<SupportTicketCategory>();
    public DbSet<SupportTicketAssignment> SupportTicketAssignments => Set<SupportTicketAssignment>();
    public DbSet<SupportChatMessage> SupportChatMessages => Set<SupportChatMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}