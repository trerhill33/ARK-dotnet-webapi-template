using ARK.WebApi.Domain.Support;

namespace ARK.WebApi.Application.Support;

public class CloseSupportTicketRequest : IRequest<Guid>
{
    public Guid TicketId { get; set; }

    public CloseSupportTicketRequest(Guid id) => TicketId = id;
}

public class CloseSupportTicketRequestHandler : IRequestHandler<CloseSupportTicketRequest, Guid>
{
    private readonly IRepositoryWithEvents<SupportTicket> _repository;
    private readonly IStringLocalizer _t;

    public CloseSupportTicketRequestHandler(IRepositoryWithEvents<SupportTicket> repository, IStringLocalizer<CloseSupportTicketRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;
    }

    public async Task<Guid> Handle(CloseSupportTicketRequest request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.TicketId, cancellationToken);

        _ = ticket ?? throw new NotFoundException(_t["Support ticket not found with ID: {0}"]);

        var result = ticket.CloseTicket();

        if(result.IsError)
            throw new ConflictException(result.FirstError.Description);

        await _repository.UpdateAsync(ticket, cancellationToken);

        return request.TicketId;
    }
}