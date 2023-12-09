using ARK.WebApi.Shared.Events;

namespace ARK.WebApi.Application.Common.Events;

public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent @event);
}