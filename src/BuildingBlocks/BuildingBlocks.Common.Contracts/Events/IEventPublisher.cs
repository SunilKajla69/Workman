namespace BuildingBlocks.Common.Contracts.Events
{
    /// <summary>
    /// Interface for publishing integration events
    /// </summary>
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) 
            where TEvent : BaseEvent;
    }
}
