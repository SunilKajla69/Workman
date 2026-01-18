namespace BuildingBlocks.Common.Contracts.Events
{
    public abstract record BaseEvent
    {
        public Guid EventId { get; init; } = Guid.NewGuid();
        public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
        public string EventType => GetType().Name;
    }
}
