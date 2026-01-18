using BuildingBlocks.Common.Contracts.Events;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Common.Infrastructure
{
    public sealed class InMemoryEventPublisher : IEventPublisher
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryEventPublisher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) 
            where TEvent : BaseEvent
        {
            using var scope = _serviceProvider.CreateScope();

            // Find all handlers for this event type
            var handlerType = typeof(IEventHandler<>).MakeGenericType(typeof(TEvent));
            var handlers = scope.ServiceProvider.GetServices(handlerType);

            // Execute all handlers asynchronously
            var tasks = handlers.Select(handler =>
                (Task)handlerType.GetMethod("HandleAsync")!
                    .Invoke(handler, new object[] { @event, cancellationToken })!);

            await Task.WhenAll(tasks);
        }
    }
}
