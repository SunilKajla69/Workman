using WORKMAN.Config.Entites.FeildTypeEntityConfig;
using WORKMAN.Config.Entites.MenuEntityConfig;
using WORKMAN.Config.Entites.AttributeGroupConfig;
using WORKMAN.Config.Entites.MenusAttributesEntityConfig;

namespace WORKMAN.Config.DataSeed
{
    public static class DataSeeder
    {
        public static void SeedData(ConfigDbContext context, ILogger logger)
        {
            SeedDefaultFieldTypes(context, logger);
            SeedMenuConfigsWithAttributesAndGroups(context, logger);
        }

        private static void SeedDefaultFieldTypes(ConfigDbContext context, ILogger logger)
        {
            try
            {
                if (!context.FieldTypeConfig.Any())
                {
                    logger.LogInformation("Seeding default FieldType configurations...");

                    var defaultFieldTypes = new List<FieldTypeConfig>
                    {
                        new FieldTypeConfig
                        {
                            Name = "text",
                            DisplayName = "Text",
                            Discription = "Single line text input field",
                            Title = "Text Field",
                            DisplayOrder = 1,
                            FieldType = "text",
                            Icon = "fa-text"
                        },
                        new FieldTypeConfig
                        {
                            Name = "number",
                            DisplayName = "Number",
                            Discription = "Numeric input field",
                            Title = "Number Field",
                            DisplayOrder = 2,
                            FieldType = "number",
                            Icon = "fa-hashtag"
                        },
                        new FieldTypeConfig
                        {
                            Name = "email",
                            DisplayName = "Email",
                            Discription = "Email address input field",
                            Title = "Email Field",
                            DisplayOrder = 3,
                            FieldType = "email",
                            Icon = "fa-envelope"
                        },
                        new FieldTypeConfig
                        {
                            Name = "date",
                            DisplayName = "Date",
                            Discription = "Date picker field",
                            Title = "Date Field",
                            DisplayOrder = 4,
                            FieldType = "date",
                            Icon = "fa-calendar"
                        },
                        new FieldTypeConfig
                        {
                            Name = "datetime",
                            DisplayName = "Date Time",
                            Discription = "Date and time picker field",
                            Title = "Date Time Field",
                            DisplayOrder = 5,
                            FieldType = "datetime",
                            Icon = "fa-clock"
                        },
                        new FieldTypeConfig
                        {
                            Name = "textarea",
                            DisplayName = "Text Area",
                            Discription = "Multi-line text input field",
                            Title = "Text Area Field",
                            DisplayOrder = 6,
                            FieldType = "textarea",
                            Icon = "fa-align-left"
                        },
                        new FieldTypeConfig
                        {
                            Name = "dropdown",
                            DisplayName = "Dropdown",
                            Discription = "Dropdown selection field",
                            Title = "Dropdown Field",
                            DisplayOrder = 7,
                            FieldType = "select",
                            Icon = "fa-caret-down"
                        },
                        new FieldTypeConfig
                        {
                            Name = "checkbox",
                            DisplayName = "Checkbox",
                            Discription = "Checkbox field for boolean values",
                            Title = "Checkbox Field",
                            DisplayOrder = 8,
                            FieldType = "checkbox",
                            Icon = "fa-check-square"
                        },
                        new FieldTypeConfig
                        {
                            Name = "radio",
                            DisplayName = "Radio Button",
                            Discription = "Radio button selection field",
                            Title = "Radio Field",
                            DisplayOrder = 9,
                            FieldType = "radio",
                            Icon = "fa-dot-circle"
                        },
                        new FieldTypeConfig
                        {
                            Name = "file",
                            DisplayName = "File Upload",
                            Discription = "File upload field",
                            Title = "File Field",
                            DisplayOrder = 10,
                            FieldType = "file",
                            Icon = "fa-upload"
                        }
                    };

                    context.FieldTypeConfig.AddRange(defaultFieldTypes);
                    context.SaveChanges();
                    logger.LogInformation("Successfully seeded {Count} default FieldType configurations.", defaultFieldTypes.Count);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to seed default FieldType configurations.");
            }
        }

        private static void SeedMenuConfigsWithAttributesAndGroups(ConfigDbContext context, ILogger logger)
        {
            try
            {
                if (!context.MenuConfig.Any())
                {
                    logger.LogInformation("Seeding MenuConfig, AttributeGroups, and MenusAttributesConfig...");

                    var menuConfigs = new List<(MenuConfig menu, string tableName, List<(string name, string displayName, string fieldType, bool isHide)> attributes)>
                    {
                        CreateMenuConfigData("MenuConfig", "Menu Configuration", "fa-sitemap", 1),
                        CreateMenuConfigData("StatusConfig", "Status Configuration", "fa-toggle-on", 2),
                        CreateMenuConfigData("DeleteStatusConfig", "Delete Status Configuration", "fa-trash", 3),
                        CreateMenuConfigData("FieldTypeConfig", "Field Type Configuration", "fa-th-list", 4),
                        CreateMenuConfigData("FilterConfig", "Filter Configuration", "fa-filter", 5),
                        CreateMenuConfigData("AdvanceFilters", "Advanced Filters", "fa-filter-circle-xmark", 6),
                        CreateMenuConfigData("AttributeGroup", "Attribute Group", "fa-object-group", 7),
                        CreateMenuConfigData("BussinesRuleConfig", "Business Rule Configuration", "fa-gavel", 8),
                        CreateMenuConfigData("MenusAttributesConfig", "Menu Attributes Configuration", "fa-list-check", 9),
                        CreateMenuConfigData("UserColumnView", "User Column View", "fa-eye", 10)
                    };

                    int menuDisplayOrder = 1;
                    foreach (var (menu, tableName, attributes) in menuConfigs)
                    {
                        // Save MenuConfig
                        context.MenuConfig.Add(menu);
                        context.SaveChanges(); // Save to get the Id

                        // Create AttributeGroup for this menu
                        var attributeGroup = new AttributeGroup
                        {
                            MenuConfigId = (int)menu.Id,
                            GroupName = $"{tableName}Group",
                            GroupDisplayName = $"{menu.MenuDisplayName} Fields",
                            GroupTitle = $"{menu.MenuTitle} Attribute Group",
                            GroupDescription = $"Attribute group for {menu.MenuDisplayName}",
                            GroupDisplayOrder = 1
                        };

                        context.AttributeGroup.Add(attributeGroup);
                        context.SaveChanges(); // Save to get the Id

                        // Create MenusAttributesConfig for each attribute
                        int attributeOrder = 1;
                        foreach (var (name, displayName, fieldType, isHide) in attributes)
                        {
                            var fieldTypeId = GetFieldTypeId(fieldType);
                            
                            var menuAttribute = new MenusAttributesConfig
                            {
                                MenuConfigId = (int)menu.Id,
                                AttributeGroupId = attributeGroup.Id.ToString(),
                                ColumnName = name,
                                AttributeName = name,
                                AttributeDisplayName = displayName,
                                AttributeValue = string.Empty,
                                AttributeDescription = $"{displayName} for {menu.MenuDisplayName}",
                                AttributeTitle = displayName,
                                ImportName = name,
                                ExportName = displayName,
                                FieldTypeId = fieldTypeId,
                                Disable = false,
                                IsHide = isHide
                            };

                            context.MenusAttributesConfig.Add(menuAttribute);
                        }

                        context.SaveChanges();
                        logger.LogInformation("Seeded menu: {MenuName} with {AttributeCount} attributes", menu.MenuName, attributes.Count);
                    }

                    logger.LogInformation("Successfully seeded all MenuConfigs, AttributeGroups, and MenusAttributesConfig.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to seed MenuConfigs with attributes and groups.");
            }
        }

        private static (MenuConfig menu, string tableName, List<(string name, string displayName, string fieldType, bool isHide)> attributes) CreateMenuConfigData(
            string tableName, 
            string displayName, 
            string icon, 
            int displayOrder)
        {
            var menu = new MenuConfig
            {
                Code = tableName.ToUpper(),
                MenuName = tableName,
                MenuDisplayName = displayName,
                MenuTitle = displayName,
                MenuIcon = icon,
                MenuPath = $"/config/{tableName.ToLower()}",
                TableName = tableName,
                Query = $"SELECT * FROM \"{tableName}\"",
                ParentMenuId = 0,
                MenuTypeId = 1,
                MenuDiscription = $"Configuration for {displayName}",
                StatusId = 1,
                DisplayOrder = displayOrder,
                IsCreatedBySystem= true
            };

            var attributes = GetAttributesForEntity(tableName);
            
            return (menu, tableName, attributes);
        }

        private static List<(string name, string displayName, string fieldType, bool isHide)> GetAttributesForEntity(string entityName)
        {
            // Base entity attributes (common to all entities that inherit from BaseEntity)
            var baseAttributes = new List<(string name, string displayName, string fieldType, bool isHide)>
            {
                ("Id", "ID", "number", false),
                ("CreatedAt", "Created At", "datetime", false),
                ("UpdatedAt", "Updated At", "datetime", false),
                ("CreatedBy", "Created By", "number", false),
                ("UpdatedBy", "Updated By", "number", false),
                ("IsDeleted", "Is Deleted", "number", false)
            };

            var specificAttributes = entityName switch
            {
                "MenuConfig" => new List<(string, string, string, bool)>
                {
                    ("Code", "Code", "text", false),
                    ("MenuName", "Menu Name", "text", false),
                    ("MenuDisplayName", "Display Name", "text", false),
                    ("MenuTitle", "Title", "text", false),
                    ("MenuIcon", "Icon", "text", false),
                    ("MenuPath", "Path", "text", false),
                    ("TableName", "Table Name", "text", false),
                    ("Query", "Query", "textarea", false),
                    ("ParentMenuId", "Parent Menu ID", "number", false),
                    ("MenuTypeId", "Menu Type ID", "number", false),
                    ("MenuDiscription", "Description", "textarea", false),
                    ("StatusId", "Status ID", "number", false),
                    ("DisplayOrder", "Display Order", "number", false)
                },
                "StatusConfig" => new List<(string, string, string, bool)>
                {
                    ("Name", "Name", "text", false),
                    ("Discription", "Description", "textarea", false),
                    ("Title", "Title", "text", false)
                },
                "DeleteStatusConfig" => new List<(string, string, string, bool)>
                {
                    ("Name", "Name", "text", false),
                    ("DisplayName", "Display Name", "text", false),
                    ("Discription", "Description", "textarea", false),
                    ("Title", "Title", "text", false)
                },
                "FieldTypeConfig" => new List<(string, string, string, bool)>
                {
                    ("Name", "Name", "text", false),
                    ("DisplayName", "Display Name", "text", false),
                    ("Discription", "Description", "textarea", false),
                    ("Title", "Title", "text", false),
                    ("DisplayOrder", "Display Order", "number", false),
                    ("FieldType", "Field Type", "text", false),
                    ("Icon", "Icon", "text", false)
                },
                "FilterConfig" => new List<(string, string, string, bool)>
                {
                    ("Name", "Name", "text", false),
                    ("FilterDisplayName", "Display Name", "text", false),
                    ("Title", "Title", "text", false),
                    ("Description", "Description", "textarea", false),
                    ("WhereConditionQuery", "Where Condition Query", "textarea", false),
                    ("WhereConditionReplacer", "Where Condition Replacer", "text", false),
                    ("DisplayOrder", "Display Order", "number", false)
                },
                "AdvanceFilters" => new List<(string, string, string, bool)>
                {
                    ("FilterName", "Filter Name", "text", false),
                    ("FilterDisplayName", "Display Name", "text", false),
                    ("FilterTitle", "Title", "text", false),
                    ("FilterDescription", "Description", "textarea", false),
                    ("DisplayOrder", "Display Order", "number", false)
                },
                "AttributeGroup" => new List<(string, string, string, bool)>
                {
                    ("MenuConfigId", "Menu Config ID", "number", false),
                    ("GroupName", "Group Name", "text", false),
                    ("GroupDisplayName", "Display Name", "text", false),
                    ("GroupTitle", "Title", "text", false),
                    ("GroupDescription", "Description", "textarea", false),
                    ("GroupDisplayOrder", "Display Order", "number", false)
                },
                "BussinesRuleConfig" => new List<(string, string, string, bool)>
                {
                    ("RuleName", "Rule Name", "text", false),
                    ("RuleDescription", "Description", "textarea", false),
                    ("RuleTitle", "Title", "text", false),
                    ("MenuConfigId", "Menu Config ID", "number", false),
                    ("RuleTypes", "Rule Types", "text", false)
                },
                "MenusAttributesConfig" => new List<(string, string, string, bool)>
                {
                    ("MenuConfigId", "Menu Config ID", "number", false),
                    ("AttributeGroupId", "Attribute Group ID", "text", false),
                    ("ColumnName", "Column Name", "text", false),
                    ("AttributeName", "Attribute Name", "text", false),
                    ("AttributeDisplayName", "Display Name", "text", false),
                    ("AttributeValue", "Value", "text", false),
                    ("AttributeDescription", "Description", "textarea", false),
                    ("AttributeTitle", "Title", "text", false),
                    ("ImportName", "Import Name", "text", false),
                    ("ExportName", "Export Name", "text", false),
                    ("FieldTypeId", "Field Type ID", "number", false),
                    ("Disable", "Disable", "checkbox", false),
                    ("IsHide", "Is Hide", "checkbox", false)
                },
                "UserColumnView" => new List<(string, string, string, bool)>
                {
                    ("ViewName", "View Name", "text", false),
                    ("ViewDisplayName", "Display Name", "text", false),
                    ("ViewDiscription", "Description", "textarea", false),
                    ("ViewTitle", "Title", "text", false),
                    ("MenuConfigId", "Menu Config ID", "number", false),
                    ("DisplayOrder", "Display Order", "number", false),
                    ("RoleId", "Role ID", "number", false),
                    ("IsSelected", "Is Selected", "checkbox", false),
                    ("ShowFilters", "Show Filters", "checkbox", false)
                },
                _ => new List<(string, string, string, bool)>()
            };

            // Combine specific attributes with base attributes
            var allAttributes = new List<(string name, string displayName, string fieldType, bool isHide)>();
            allAttributes.AddRange(specificAttributes);
            allAttributes.AddRange(baseAttributes);
            
            return allAttributes;
        }

        private static int GetFieldTypeId(string fieldType)
        {
            return fieldType switch
            {
                "text" => 1,
                "number" => 2,
                "email" => 3,
                "date" => 4,
                "datetime" => 5,
                "textarea" => 6,
                "select" => 7,
                "checkbox" => 8,
                "radio" => 9,
                "file" => 10,
                _ => 1 // default to text
            };
        }
    }
}
