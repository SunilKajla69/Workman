using MapsterMapper;
using WORKMAN.Config.Entites.MenuEntityConfig;
using WORKMAN.Config.ViewModels.MenuConfigViewModels;

namespace WORKMAN.Config.Examples
{
    /// <summary>
    /// Example class demonstrating how to use Mapster for mapping between entities and ViewModels
    /// </summary>
    public class MapsterUsageExample
    {
        private readonly IMapper _mapper;
        private readonly ConfigDbContext _context;

        public MapsterUsageExample(IMapper mapper, ConfigDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Example: Convert Entity to ViewModel
        /// </summary>
        public async Task<MenuConfigVM?> GetMenuConfigAsync(int id)
        {
            var menuConfig = await _context.MenuConfig.FindAsync(id);
            
            if (menuConfig == null)
                return null;

            // Map entity to ViewModel
            var viewModel = _mapper.Map<MenuConfigVM>(menuConfig);
            return viewModel;
        }

        /// <summary>
        /// Example: Convert ViewModel to Entity for creating new record
        /// </summary>
        public async Task<int> CreateMenuConfigAsync(MenuConfigVM viewModel)
        {
            // Map ViewModel to entity
            var menuConfig = _mapper.Map<MenuConfig>(viewModel);
            
            _context.MenuConfig.Add(menuConfig);
            await _context.SaveChangesAsync();
            
            return menuConfig.Id;
        }

        /// <summary>
        /// Example: Get list of entities and map to ViewModels
        /// </summary>
        public async Task<List<MenuConfigVM>> GetAllMenuConfigsAsync()
        {
            var menuConfigs = await _context.MenuConfig.ToListAsync();
            
            // Map list of entities to list of ViewModels
            var viewModels = _mapper.Map<List<MenuConfigVM>>(menuConfigs);
            return viewModels;
        }

        /// <summary>
        /// Example: Update existing entity using ViewModel
        /// </summary>
        public async Task<bool> UpdateMenuConfigAsync(int id, MenuConfigVM viewModel)
        {
            var existingEntity = await _context.MenuConfig.FindAsync(id);
            
            if (existingEntity == null)
                return false;

            // Map ViewModel to existing entity (Mapster will update the properties)
            _mapper.Map(viewModel, existingEntity);
            
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
