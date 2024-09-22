using clean_keycloak_pkce.Domain.Events;
using Microsoft.Extensions.Logging;

namespace clean_keycloak_pkce.Application.TodoItems.EventHandlers;

public class TodoItemCreatedEventHandler : INotificationHandler<TodoItemCreatedEvent>
{
    private readonly ILogger<TodoItemCreatedEventHandler> _logger;

    public TodoItemCreatedEventHandler(ILogger<TodoItemCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("clean_keycloak_pkce Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
