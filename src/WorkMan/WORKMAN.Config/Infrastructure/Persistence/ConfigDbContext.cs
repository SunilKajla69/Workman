using WORKMAN.Config.Entites.StatusEntityConfig;
using WORKMAN.Config.Entites.UserColumnView;

namespace WORKMAN.Config.Infrastructure.Persistence
{
    public class ConfigDbContext : DbContext
    {
        public ConfigDbContext(DbContextOptions<ConfigDbContext> options) : base(options)
        {
        }

        public DbSet<MenuConfig> MenuConfig => Set<MenuConfig>();
        public DbSet<StatusConfig> StatusConfig => Set<StatusConfig>();
        public DbSet<DeleteStatusConfig> DeleteStatusConfig => Set<DeleteStatusConfig>();
        public DbSet<MenuConfigLogs> MenuConfigLogs => Set<MenuConfigLogs>();
        public DbSet<MenusAttributesConfig> MenusAttributesConfig => Set<MenusAttributesConfig>();
        public DbSet<BussinesRuleConfig> BussinesRuleConfig => Set<BussinesRuleConfig>();
        public DbSet<BussinesRuleAttributes> BussinesRuleAttributes => Set<BussinesRuleAttributes>();
        public DbSet<FilterConfig> FilterConfig => Set<FilterConfig>();
        public DbSet<UserColumnView> UserColumnView => Set<UserColumnView>();
        public DbSet<AttributeGroup> AttributeGroup => Set<AttributeGroup>();
        public DbSet<AdvanceFilters> AdvanceFilters => Set<AdvanceFilters>();
        public DbSet<FieldTypeConfig> FieldTypeConfig => Set<FieldTypeConfig>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ConfigDbContext).Assembly);
        }
    }
}
