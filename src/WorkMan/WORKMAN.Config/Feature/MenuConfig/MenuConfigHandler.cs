using Mapster;
using Microsoft.EntityFrameworkCore;
using WORKMAN.Config.Infrastructure.Persistence;
using WORKMAN.Config.ViewModels.AttributeViewModel;
using WORKMAN.Config.ViewModels.FieldTypeViewModels;
using WORKMAN.Config.ViewModels.MenuConfigViewModels;

namespace WORKMAN.Config.Feature.MenuConfig
{
    public class MenuConfigHandler
    {
        private readonly ConfigDbContext ConfigDbContext;
        public MenuConfigHandler(ConfigDbContext configDbContext)
        {
            ConfigDbContext = configDbContext;
        }

        public async Task<List<MenuConfigVM>> Getmenus( CancellationToken cancellationToken)
        {
            var menuConfigs = await ConfigDbContext.MenuConfig
                     .ToListAsync(cancellationToken);
            return menuConfigs.Adapt<List<MenuConfigVM>>();
        }
        public async Task<int> SaveUpdateMenu(List<AttributeVM> attributeVMs, CancellationToken cancellationToken)
        {
            var menuConfigs = await ConfigDbContext.MenuConfig
                     .ToListAsync(cancellationToken);
            return 1;
        }
    }
}


