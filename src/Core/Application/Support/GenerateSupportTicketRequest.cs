using ARK.WebApi.Application.Support.Jobs;

namespace ARK.WebApi.Application.Support;

public class GenerateSupportTicketRequest : IRequest<string>
{
    public DefaultIdType SubmitterId { get; set; }
    public DefaultIdType CategoryId { get; set; }
    public string Title { get; set; } = default!;
}

public class GenerateSupportTicketRequestHandler : IRequestHandler<GenerateSupportTicketRequest, string>
{
    private readonly IJobService _jobService;

    public GenerateSupportTicketRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(GenerateSupportTicketRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Schedule<ISupportTicketJob>(x => x.GenerateSupportTicket(request.SubmitterId, request.CategoryId, request.Title, cancellationToken), TimeSpan.FromSeconds(5));
        return Task.FromResult(jobId);
    }
}