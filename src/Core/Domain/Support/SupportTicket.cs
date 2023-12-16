using ErrorOr;

namespace ARK.WebApi.Domain.Support;
public class SupportTicket : AuditableEntity, IAggregateRoot
{
    public DefaultIdType SubmitterUserId { get; set; } = default!;

    public DefaultIdType CategoryId { get; set; } = default!;

    public string Title { get; set; } = default!;

    public SupportTicketStatus Status { get; set; }

    public DateTime? ClosedDate { get; set; }

    public virtual SupportTicketCategory? Category { get; set; }

    public virtual ICollection<SupportChatMessage>? ChatMessages { get; set; }

    public virtual ICollection<SupportTicketAssignment>? Assignments { get; set; }

    public SupportTicket(
        DefaultIdType submitterUserId,
        DefaultIdType categoryId,
        string title)
    {
        SubmitterUserId = submitterUserId;
        CategoryId = categoryId;
        Title = title;
        Status = SupportTicketStatus.Submitted;
        ChatMessages = new List<SupportChatMessage>();
        Assignments = new List<SupportTicketAssignment>();
    }

    public ErrorOr<Success> AddChatMessage(DefaultIdType senderUserId, string messageText)
    {
        ChatMessages.Add(new SupportChatMessage
        {
            SenderUserId = senderUserId,
            MessageText = messageText,
            SupportTicket = this
        });

        return Result.Success;
    }

    public ErrorOr<Success> AssignToSupportStaff(DefaultIdType supportStaffId)
    {
        if (Status != SupportTicketStatus.Submitted)
            return SupportErrors.CannotAddSupportStaffToAnAssignedTicket;

        if (Assignments?.Any(a => a.SupportStaffId == supportStaffId) == true)
            return SupportErrors.SupportStaffMemberAlreadyAssigned;

        Assignments.Add(new SupportTicketAssignment
        {
            SupportStaffId = supportStaffId,
            AssignedDate = DateTime.UtcNow,
            SupportTicket = this,
        });

        Status = SupportTicketStatus.InProgress;

        return Result.Success;
    }

    public ErrorOr<Success> AddAdditionalSupportStaff(DefaultIdType additionalSupportStaffId)
    {
        if (Status != SupportTicketStatus.InProgress)
            return SupportErrors.CannotAddAdditionalSupportStaffToAnUnassignedTicket;

        if (Assignments?.Any(a => a.SupportStaffId == additionalSupportStaffId) == true)
            return SupportErrors.SupportStaffMemberAlreadyAssigned;

        var additionalAssignment = new SupportTicketAssignment
        {
            SupportStaffId = additionalSupportStaffId,
            AssignedDate = DateTime.UtcNow,
            SupportTicket = this,
        };

        Assignments.Add(additionalAssignment);

        return Result.Success;
    }

    public ErrorOr<Success> ReleaseAssignment(DefaultIdType supportStaffId)
    {
        if(Assignments is null)
            return SupportErrors.NoActiveAssignmentFound;

        var currentAssignment = Assignments
            .FirstOrDefault(a => a.SupportStaffId == supportStaffId);

        if (currentAssignment != null)
            currentAssignment.ReleasedDate = DateTime.UtcNow;

        return Result.Success;
    }

    public ErrorOr<Success> ResolveTicket()
    {
        Status = SupportTicketStatus.Resolved;
        ClosedDate = DateTime.UtcNow;
        return Result.Success;
    }

    public ErrorOr<Success> CloseTicket()
    {
        if (DateTime.UtcNow < CreatedOn.AddDays(7))
            return SupportErrors.CannotCloseTicketWithinOneWeek;

        Status = SupportTicketStatus.Closed;
        ClosedDate = DateTime.UtcNow;
        return Result.Success;
    }
}
