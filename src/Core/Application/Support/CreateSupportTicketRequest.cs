using ARK.WebApi.Domain.Support;

namespace ARK.WebApi.Application.Support;
public class CreateSupportTicketRequest : IRequest<Guid>
{
    public DefaultIdType SubmitterId { get; set; }
    public DefaultIdType CategoryId { get; set; }
    public string Title { get; set; } = default!;
}

public class CreateSupportTicketRequestHandler : IRequestHandler<CreateSupportTicketRequest, Guid>
{
    private readonly IRepositoryWithEvents<SupportTicket> _repository;

    public CreateSupportTicketRequestHandler(IRepositoryWithEvents<SupportTicket> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateSupportTicketRequest request, CancellationToken cancellationToken)
    {
        var supportTicket = new SupportTicket(
            request.SubmitterId,
            request.CategoryId,
            request.Title);

        await _repository.AddAsync(supportTicket, cancellationToken);

        return supportTicket.Id;
    }
}
