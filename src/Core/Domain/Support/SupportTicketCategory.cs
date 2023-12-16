namespace ARK.WebApi.Domain.Support;
public class SupportTicketCategory : AuditableEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public SupportTicketCategory(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
