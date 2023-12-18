using System.ComponentModel;

namespace ARK.WebApi.Application.Support.Jobs;
public interface ISupportTicketJob : IScopedService
{
    [DisplayName(displayName: "Generate new Support Ticket")]
    Task GenerateSupportTicket(DefaultIdType submitterId, DefaultIdType catagoryId, string title, CancellationToken cancellationToken);

    [DisplayName(displayName: "Automatically assign support staff to support ticket")]
    Task AutoAssignSupportTicket(DefaultIdType supportTicketId, CancellationToken cancellationToken);

    [DisplayName(displayName: "Automatically close support ticket")]
    Task AutoCloseSupportTicket(DefaultIdType supportTicketId, CancellationToken cancellationToken);
}
