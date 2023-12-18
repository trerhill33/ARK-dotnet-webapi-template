using ARK.WebApi.Application.Common.Interfaces;
using ARK.WebApi.Application.Support;
using ARK.WebApi.Application.Support.Jobs;
using ARK.WebApi.Shared.Notifications;
using Hangfire;
using Hangfire.Console.Extensions;
using Hangfire.Console.Progress;
using Hangfire.Server;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ARK.WebApi.Infrastructure.Features.Support;
public class SupportTicketJob : ISupportTicketJob
{
    private readonly ILogger<SupportTicketJob> _logger;
    private readonly ISender _mediator;
    private readonly IProgressBarFactory _progressBar;
    private readonly PerformingContext _performingContext;
    private readonly INotificationSender _notifications;
    private readonly ICurrentUser _currentUser;
    private readonly IProgressBar _progress;

    public SupportTicketJob(
    ILogger<SupportTicketJob> logger,
    ISender mediator,
    IProgressBarFactory progressBar,
    PerformingContext performingContext,
    INotificationSender notifications,
    ICurrentUser currentUser)
    {
        _logger = logger;
        _mediator = mediator;
        _progressBar = progressBar;
        _performingContext = performingContext;
        _notifications = notifications;
        _currentUser = currentUser;
        _progress = _progressBar.Create();
    }

    [Queue("notdefault")]
    [AutomaticRetry(Attempts = 3)]
    public async Task GenerateSupportTicket(DefaultIdType submitterId, DefaultIdType catagoryId, string title, CancellationToken cancellationToken)
    {
        await NotifyAsync("Your support ticket submission has started", 0, cancellationToken);

        await _mediator.Send(request: new CreateSupportTicketRequest { SubmitterId = submitterId, CategoryId = catagoryId, Title = title }, cancellationToken);

        await NotifyAsync("Your support ticket has been submitted and awaiting assignment", 0, cancellationToken);
    }

    [Queue("notdefault")]
    [AutomaticRetry(Attempts = 3)]
    public async Task AutoAssignSupportTicket(DefaultIdType supportTicketId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Initializing AutoAssignSupportTicket Job with Id: {jobId}", _performingContext.BackgroundJob.Id);

        await _mediator.Send(new AutoAssignSupportTicketRequest { TicketId = supportTicketId }, cancellationToken);

        _logger.LogInformation("AutoAssignSupportTicket Job with Id: {jobId} completed", _performingContext.BackgroundJob.Id);
    }

    [Queue("notdefault")]
    [AutomaticRetry(Attempts = 5)]
    public async Task AutoCloseSupportTicket(DefaultIdType supportTicketId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Initializing AutoCloseSupportTicket Job with Id: {jobId}", _performingContext.BackgroundJob.Id);

        await _mediator.Send(request: new CloseSupportTicketRequest { TicketId = supportTicketId }, cancellationToken);

        _logger.LogInformation("AutoCloseSupportTicket Job with Id: {jobId} completed", _performingContext.BackgroundJob.Id);
    }

    private async Task NotifyAsync(string message, int progress, CancellationToken cancellationToken)
    {
        _progress.SetValue(progress);
        await _notifications.SendToUserAsync(
            new JobNotification()
            {
                JobId = _performingContext.BackgroundJob.Id,
                Message = message,
                Progress = progress
            },
            _currentUser.GetUserId().ToString(),
            cancellationToken);
    }
}
