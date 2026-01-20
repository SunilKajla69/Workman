using WORKMAN.Config.Infrastructure.Persistence;
using WORKMAN.Config.ViewModels.FieldTypeViewModels;

namespace WORKMAN.Config.Feature.FieldTypeConfig
{
    public class FieldTypeHandler
    {
        private readonly ConfigDbContext ConfigDbContext;
        public FieldTypeHandler(ConfigDbContext configDbContext)
        {
            ConfigDbContext = configDbContext;
        }

        public async Task<List<string>> HandleAddUpdateFieldAsync(FieldTypeVM request, CancellationToken cancellationToken)
        {
            //var fieldTypes = await ConfigDbContext.FieldTypeConfig
            //    .Select(ft => ft.FieldTypeName)
            //    .ToListAsync(cancellationToken);
            return new List<string>();
        }
    }
}
