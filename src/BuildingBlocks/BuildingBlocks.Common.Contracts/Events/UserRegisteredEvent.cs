namespace BuildingBlocks.Common.Contracts.Events
{
    public sealed record UserRegisteredEvent : BaseEvent
    {
        public Guid UserId { get; init; }
        public string Email { get; init; }
        public DateTime RegisteredAt { get; init; }
    }
}
