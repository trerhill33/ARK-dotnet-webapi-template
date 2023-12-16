namespace ARK.WebApi.Domain.Support;
public class SupportTicketAssignment : AuditableEntity
{
    public DefaultIdType SupportStaffId { get; set; } = default!; // ID of the support staff assigned to ticket

    public DefaultIdType SupportTicketId { get; set; }

    public DateTime? AssignedDate { get; set; }

    public DateTime? ReleasedDate { get; set; }

    public required SupportTicket SupportTicket { get; set; }
}
