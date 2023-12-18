namespace ARK.WebApi.Application.Support;
internal class AssignSupportTicketRequest : IRequest<string>
{
    public DefaultIdType TicketId { get; set; }
    public DefaultIdType SupportStaffId { get; set; }
}
