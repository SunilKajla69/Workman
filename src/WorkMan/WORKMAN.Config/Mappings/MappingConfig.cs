namespace WORKMAN.Config.Mappings
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // MenuConfig mappings
            config.NewConfig<MenuConfig, MenuConfigVM>();
            config.NewConfig<MenuConfigVM, MenuConfig>()
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UpdatedAt)
                .Ignore(dest => dest.CreatedBy)
                .Ignore(dest => dest.UpdatedBy)
                .Ignore(dest => dest.IsDeleted);

            // StatusConfig mappings
            config.NewConfig<StatusConfig, StatusConfigVM>();
            config.NewConfig<StatusConfigVM, StatusConfig>()
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UpdatedAt)
                .Ignore(dest => dest.CreatedBy)
                .Ignore(dest => dest.UpdatedBy)
                .Ignore(dest => dest.IsDeleted);

            // DeleteStatusConfig mappings
            config.NewConfig<DeleteStatusConfig, DeleteStatusConfigVM>();
            config.NewConfig<DeleteStatusConfigVM, DeleteStatusConfig>()
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UpdatedAt)
                .Ignore(dest => dest.CreatedBy)
                .Ignore(dest => dest.UpdatedBy)
                .Ignore(dest => dest.IsDeleted);

            // MenuConfigLogs mappings
            config.NewConfig<MenuConfigLogs, MenuConfigLogsVM>();
            config.NewConfig<MenuConfigLogsVM, MenuConfigLogs>();

            // MenusAttributesConfig mappings
            config.NewConfig<MenusAttributesConfig, MenusAttributesConfigVM>();
            config.NewConfig<MenusAttributesConfigVM, MenusAttributesConfig>()
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UpdatedAt)
                .Ignore(dest => dest.CreatedBy)
                .Ignore(dest => dest.UpdatedBy)
                .Ignore(dest => dest.IsDeleted);

            // BussinesRuleConfig mappings
            config.NewConfig<BussinesRuleConfig, BussinesRuleConfigVM>();
            config.NewConfig<BussinesRuleConfigVM, BussinesRuleConfig>()
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UpdatedAt)
                .Ignore(dest => dest.CreatedBy)
                .Ignore(dest => dest.UpdatedBy)
                .Ignore(dest => dest.IsDeleted);

            // BussinesRuleAttributes mappings
            config.NewConfig<BussinesRuleAttributes, BussinesRuleAttributesVM>();
            config.NewConfig<BussinesRuleAttributesVM, BussinesRuleAttributes>()
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UpdatedAt)
                .Ignore(dest => dest.CreatedBy)
                .Ignore(dest => dest.UpdatedBy)
                .Ignore(dest => dest.IsDeleted);

            // FilterConfig mappings
            config.NewConfig<FilterConfig, FilterConfigVM>();
            config.NewConfig<FilterConfigVM, FilterConfig>()
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UpdatedAt)
                .Ignore(dest => dest.CreatedBy)
                .Ignore(dest => dest.UpdatedBy)
                .Ignore(dest => dest.IsDeleted);

            // UserColumnView mappings
            config.NewConfig<UserColumnView, UserColumnViewVM>();
            config.NewConfig<UserColumnViewVM, UserColumnView>();

            // AttributeGroup mappings
            config.NewConfig<AttributeGroup, AttributeGroupVM>();
            config.NewConfig<AttributeGroupVM, AttributeGroup>()
                .Ignore(dest => dest.CreatedAt)
                .Ignore(dest => dest.UpdatedAt)
                .Ignore(dest => dest.CreatedBy)
                .Ignore(dest => dest.UpdatedBy)
                .Ignore(dest => dest.IsDeleted);

            // AdvanceFilters mappings
            config.NewConfig<AdvanceFilters, AdvanceFiltersVM>();
            config.NewConfig<AdvanceFiltersVM, AdvanceFilters>();
        }
    }
}
