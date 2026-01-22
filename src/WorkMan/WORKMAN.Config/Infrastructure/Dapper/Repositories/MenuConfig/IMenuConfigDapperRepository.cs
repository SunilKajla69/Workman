namespace WORKMAN.Config.Infrastructure.Dapper.Repositories.MenuConfig
{
    /// <summary>
    /// Interface for Menu Config repository using Dapper for stored procedures
    /// </summary>
    public interface IMenuConfigDapperRepository
    {
        /// <summary>
        /// Example: Get all menus using stored procedure
        /// </summary>
        Task<IEnumerable<ViewModels.MenuConfigViewModels.MenuConfigVM>> GetAllMenusAsync(
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Example: Get menu by ID using stored procedure
        /// </summary>
        Task<ViewModels.MenuConfigViewModels.MenuConfigVM?> GetMenuByIdAsync(
            long menuId, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Example: Create or update menu using stored procedure
        /// </summary>
        Task<int> UpsertMenuAsync(
            ViewModels.MenuConfigViewModels.MenuConfigVM menu, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Example: Delete menu using stored procedure
        /// </summary>
        Task<int> DeleteMenuAsync(
            long menuId, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Example: Get menu count using stored procedure
        /// </summary>
        Task<int> GetMenuCountAsync(
            CancellationToken cancellationToken = default);
    }
}
