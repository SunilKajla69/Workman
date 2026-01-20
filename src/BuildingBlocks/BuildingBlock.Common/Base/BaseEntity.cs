namespace BuildingBlocks.Common.Base
{
    public abstract class BaseEntity
    {
        public long Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; protected set; }
        public int UpdatedBy { get; protected set; }
        public int CreatedBy { get; protected set; }
        public int IsDeleted { get; protected set; }

        public void MarkAsDeleted(int updatedBy)
        {
            if (IsDeleted == 1)
                return;

            IsDeleted = 1;
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate(int updatedBy)
        {
            IsDeleted = 1;
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
