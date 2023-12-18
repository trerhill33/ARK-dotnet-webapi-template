using ARK.WebApi.Domain.Support;

namespace ARK.WebApi.Application.Support.Specs;

public class SupportTicketByUserSpec : Specification<SupportTicket>
{
    public SupportTicketByUserSpec(DefaultIdType userId) =>
        Query.Where(b => b.SubmitterUserId == userId);
}
