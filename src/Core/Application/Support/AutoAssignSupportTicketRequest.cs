namespace ARK.WebApi.Application.Support;

public class AutoAssignSupportTicketRequest : IRequest<string>
{
    public DefaultIdType TicketId { get; set; }
    public DefaultIdType SupportStaffId { get; set; }
}
