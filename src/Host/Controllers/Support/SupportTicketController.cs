using ARK.WebApi.Application.Support;

namespace ARK.WebApi.Host.Controllers.Support;
public class SupportTicketController : VersionedApiController
{
    [HttpPost("generate-support-ticket")]
    [MustHavePermission(ARKAction.Generate, ARKResource.Support)]
    [OpenApiOperation("Generate a Support Ticket.", "")]
    public Task<string> GenerateRandomAsync(GenerateSupportTicketRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("resolve-support-ticket/{id:guid})")]
    [MustHavePermission(ARKAction.Update, ARKResource.Support)]
    [OpenApiOperation("Resolve a Support Ticket.", "")]
    public Task<Guid> ResolveSupportTicketAsync(Guid id)
    {
        return Mediator.Send(new ResolveSupportTicketRequest(id));
    }
}
