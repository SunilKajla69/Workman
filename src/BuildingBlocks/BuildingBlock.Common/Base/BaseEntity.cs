namespace BuildingBlocks.Common.Base
{
    public abstract class BaseEntity
    {
        public long Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; protected set; }

        protected void MarkAsModified()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
