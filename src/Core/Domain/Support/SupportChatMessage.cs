namespace ARK.WebApi.Domain.Support;
public class SupportChatMessage : AuditableEntity
{
    public DefaultIdType SenderUserId { get; set; }

    public string MessageText { get; set; } = default!;

    public DefaultIdType SupportTicketId { get; set; }

    public required SupportTicket SupportTicket { get; set; }
}
