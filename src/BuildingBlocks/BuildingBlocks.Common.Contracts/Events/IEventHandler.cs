namespace BuildingBlocks.Common.Contracts.Events
{

    /// <summary>
    /// Interface for handling integration events
    /// </summary>
    public interface IEventHandler<in TEvent> where TEvent : BaseEvent
    {
        Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
    }
}
