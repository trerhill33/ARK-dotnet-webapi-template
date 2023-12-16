using ARK.WebApi.Domain.Support;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARK.WebApi.Infrastructure.Persistence.Configuration;

public class SupportTicketConfig : IEntityTypeConfiguration<SupportTicket>
{
    public void Configure(EntityTypeBuilder<SupportTicket> builder)
    {
        builder
            .ToTable("Tickets", SchemaNames.Support)
            .IsMultiTenant();
    }
}

public class SupportTicketCategoryConfig : IEntityTypeConfiguration<SupportTicketCategory>
{
    public void Configure(EntityTypeBuilder<SupportTicketCategory> builder)
    {
        builder
            .ToTable("Categories", SchemaNames.Support)
            .IsMultiTenant();
    }
}

public class SupportChatMessageConfig : IEntityTypeConfiguration<SupportChatMessage>
{
    public void Configure(EntityTypeBuilder<SupportChatMessage> builder)
    {
        builder
            .ToTable("Chats", SchemaNames.Support)
            .IsMultiTenant();
    }
}

public class SupportTicketAssignmentConfig : IEntityTypeConfiguration<SupportTicketAssignment>
{
    public void Configure(EntityTypeBuilder<SupportTicketAssignment> builder)
    {
        builder
            .ToTable("Assignments", SchemaNames.Support)
            .IsMultiTenant();
    }
}