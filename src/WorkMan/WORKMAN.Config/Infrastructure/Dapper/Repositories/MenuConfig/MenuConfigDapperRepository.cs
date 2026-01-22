using WORKMAN.Config.ViewModels.MenuConfigViewModels;

namespace WORKMAN.Config.Infrastructure.Dapper.Repositories.MenuConfig
{
    /// <summary>
    /// Implementation of Menu Config repository using Dapper for stored procedures
    /// This is an EXAMPLE showing how to use the base DapperRepository
    /// </summary>
    public class MenuConfigDapperRepository : IMenuConfigDapperRepository
    {
        private readonly IDapperRepository _dapperRepository;
        private readonly ILogger<MenuConfigDapperRepository> _logger;

        public MenuConfigDapperRepository(
            IDapperRepository dapperRepository,
            ILogger<MenuConfigDapperRepository> logger)
        {
            _dapperRepository = dapperRepository;
            _logger = logger;
        }

        /// <summary>
        /// Example: Get all menus using stored procedure
        /// Stored Procedure Name: sp_GetAllMenus (you need to create this in your database)
        /// </summary>
        public async Task<IEnumerable<MenuConfigVM>> GetAllMenusAsync(
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching all menus using stored procedure");

            // Call stored procedure without parameters
            var result = await _dapperRepository.ExecuteStoredProcedureAsync<MenuConfigVM>(
                "sp_GetAllMenus",
                parameters: null,
                cancellationToken: cancellationToken);

            return result;
        }

        /// <summary>
        /// Example: Get menu by ID using stored procedure
        /// Stored Procedure Name: sp_GetMenuById (you need to create this in your database)
        /// Parameters: @MenuId
        /// </summary>
        public async Task<MenuConfigVM?> GetMenuByIdAsync(
            long menuId,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Fetching menu with ID: {MenuId}", menuId);

            // Call stored procedure with parameters
            var parameters = new { MenuId = menuId };
            
            var result = await _dapperRepository.ExecuteStoredProcedureSingleAsync<MenuConfigVM>(
                "sp_GetMenuById",
                parameters: parameters,
                cancellationToken: cancellationToken);

            return result;
        }

        /// <summary>
        /// Example: Create or update menu using stored procedure
        /// Stored Procedure Name: sp_UpsertMenu (you need to create this in your database)
        /// Parameters: All menu properties
        /// Returns: Affected rows count
        /// </summary>
        public async Task<int> UpsertMenuAsync(
            MenuConfigVM menu,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Upserting menu: {MenuName}", menu.MenuName);

            // Map view model to parameters
            var parameters = new
            {
                menu.Id,
                menu.MenuName,
                menu.MenuShortName,
                menu.MenuDisplayName,
                menu.MenuIcon,
                menu.MenuRoute,
                menu.MenuParentId,
                menu.MenuDisplayOrder,
                menu.IsSystemMenu,
                menu.IsActive,
                menu.IsDeleted
            };

            var affectedRows = await _dapperRepository.ExecuteStoredProcedureNonQueryAsync(
                "sp_UpsertMenu",
                parameters: parameters,
                cancellationToken: cancellationToken);

            return affectedRows;
        }

        /// <summary>
        /// Example: Delete menu using stored procedure
        /// Stored Procedure Name: sp_DeleteMenu (you need to create this in your database)
        /// Parameters: @MenuId
        /// Returns: Affected rows count
        /// </summary>
        public async Task<int> DeleteMenuAsync(
            long menuId,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Deleting menu with ID: {MenuId}", menuId);

            var parameters = new { MenuId = menuId };

            var affectedRows = await _dapperRepository.ExecuteStoredProcedureNonQueryAsync(
                "sp_DeleteMenu",
                parameters: parameters,
                cancellationToken: cancellationToken);

            return affectedRows;
        }

        /// <summary>
        /// Example: Get menu count using stored procedure
        /// Stored Procedure Name: sp_GetMenuCount (you need to create this in your database)
        /// Returns: Total count of menus
        /// </summary>
        public async Task<int> GetMenuCountAsync(
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting total menu count");

            var count = await _dapperRepository.ExecuteStoredProcedureScalarAsync<int>(
                "sp_GetMenuCount",
                parameters: null,
                cancellationToken: cancellationToken);

            return count;
        }
    }
}
