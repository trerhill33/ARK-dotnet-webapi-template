using ErrorOr;

namespace ARK.WebApi.Domain.Support;
public static class SupportErrors
{
    public static readonly Error CannotAddAdditionalSupportStaffToAnUnassignedTicket = Error.Validation(
        code: "Support.CannotAddAdditionalSupportStaffToAnUnassignedTicket",
        description: "Unable to add additional support staff to an unassigned ticket");

    public static readonly Error CannotAddSupportStaffToAnAssignedTicket = Error.Validation(
        code: "Support.CannotAddSupportStaffToAnAssignedTicket",
        description: "Unable to add support staff to an assigned ticket");

    public static readonly Error SupportStaffMemberAlreadyAssigned = Error.Validation(
     code: "Support.SupportStaffMemberAlreadyAssigned",
     description: "The support staff member is already assigned to this ticket.");

    public static readonly Error NoActiveAssignmentFound = Error.Validation(
        code: "Support.NoActiveAssignmentFound",
        description: "No active assignment found for the specified support staff member.");

    public static readonly Error CannotCloseTicketWithinOneWeek = Error.Validation(
        code: "Support.CannotCloseTicketWithinOneWeek",
        description: "A ticket cannot be closed within one week of its open date.");
}
