using ARK.WebApi.Domain.Support;

namespace ARK.WebApi.Application.Support;

public class ResolveSupportTicketRequest : IRequest<Guid>
{
    public Guid TicketId { get; set; }

    public ResolveSupportTicketRequest(Guid id) => TicketId = id;
}

public class ResolveSupportTicketRequestHandler : IRequestHandler<ResolveSupportTicketRequest, Guid>
{
    private readonly IRepositoryWithEvents<SupportTicket> _repository;
    private readonly IStringLocalizer _t;

    public ResolveSupportTicketRequestHandler(IRepositoryWithEvents<SupportTicket> repository, IStringLocalizer<ResolveSupportTicketRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;
    }

    public async Task<Guid> Handle(ResolveSupportTicketRequest request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.TicketId, cancellationToken);

        _ = ticket ?? throw new NotFoundException(_t["Support ticket not found with ID: {0}"]);

        var result = ticket.ResolveTicket();

        if (result.IsError)
            throw new ConflictException(result.FirstError.Description);

        await _repository.UpdateAsync(ticket, cancellationToken);

        return request.TicketId;
    }
}