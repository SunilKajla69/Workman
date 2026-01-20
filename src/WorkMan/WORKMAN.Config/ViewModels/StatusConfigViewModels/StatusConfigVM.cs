using BuildingBlocks.Common.ViewModels.BaseViewModels;

namespace WORKMAN.Config.ViewModels.StatusConfigViewModels
{
    public class StatusConfigVM : BaseVM
    {
        public string Name { get; set; } = string.Empty;
        public string Discription { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
