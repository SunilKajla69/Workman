

namespace BuildingBlocks.Common.ViewModels.BaseViewModels
{
    public class BaseVM
    {
        public string Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; protected set; }
        public int UpdatedBy { get; protected set; }
        public int CreatedBy { get; protected set; }
        public int IsDeleted { get; protected set; }
    }
}
