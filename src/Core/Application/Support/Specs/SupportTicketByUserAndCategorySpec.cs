using ARK.WebApi.Domain.Support;

namespace ARK.WebApi.Application.Support.Specs;

public class SupportTicketByUserAndCategorySpec : Specification<SupportTicket>
{
    public SupportTicketByUserAndCategorySpec(DefaultIdType userId, DefaultIdType categoryId, SupportTicketStatus openStatus) =>
        Query.Where(b => b.SubmitterUserId == userId && b.CategoryId == categoryId && b.Status == openStatus);
}
