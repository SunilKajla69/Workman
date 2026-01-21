using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WORKMAN.Config.Migrations
{
    /// <inheritdoc />
    public partial class initialmigratin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvanceFilters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilterName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FilterDisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FilterTitle = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    FilterDescription = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvanceFilters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeGroup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MenuConfigId = table.Column<int>(type: "integer", nullable: false),
                    GroupName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    GroupDisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    GroupTitle = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    GroupDescription = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    GroupDisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BussinesRuleAttributes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BussinesRuleAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BussinesRuleConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RuleName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    RuleDescription = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    RuleTitle = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    MenuConfigId = table.Column<int>(type: "integer", nullable: false),
                    RuleTypes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BussinesRuleConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeleteStatusConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Discription = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Title = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeleteStatusConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FieldTypeConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    Discription = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    FieldType = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldTypeConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilterConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FilterDisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    WhereConditionQuery = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    WhereConditionReplacer = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MenuName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    MenuDisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    MenuTitle = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    MenuIcon = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MenuPath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    TableName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Query = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    ParentMenuId = table.Column<int>(type: "integer", nullable: false),
                    MenuTypeId = table.Column<int>(type: "integer", nullable: false),
                    MenuDiscription = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuConfigLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MenuConfigId = table.Column<int>(type: "integer", nullable: false),
                    ColumnName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Action = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PreviousValue = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    NewValue = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuConfigLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenusAttributesConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MenuConfigId = table.Column<int>(type: "integer", nullable: false),
                    AttributeGroupId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ColumnName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    AttributeName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    AttributeDisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    AttributeValue = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    AttributeDescription = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    AttributeTitle = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    ImportName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ExportName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FieldTypeId = table.Column<int>(type: "integer", nullable: false),
                    Disable = table.Column<bool>(type: "boolean", nullable: false),
                    IsHide = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenusAttributesConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Discription = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Title = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserColumnView",
                columns: table => new
                {
                    ViewName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    MenuConfigId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ViewDisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ViewDiscription = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ViewTitle = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsSelected = table.Column<bool>(type: "boolean", nullable: false),
                    ShowFilters = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserColumnView", x => new { x.MenuConfigId, x.RoleId, x.ViewName });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvanceFilters");

            migrationBuilder.DropTable(
                name: "AttributeGroup");

            migrationBuilder.DropTable(
                name: "BussinesRuleAttributes");

            migrationBuilder.DropTable(
                name: "BussinesRuleConfig");

            migrationBuilder.DropTable(
                name: "DeleteStatusConfig");

            migrationBuilder.DropTable(
                name: "FieldTypeConfig");

            migrationBuilder.DropTable(
                name: "FilterConfig");

            migrationBuilder.DropTable(
                name: "MenuConfig");

            migrationBuilder.DropTable(
                name: "MenuConfigLogs");

            migrationBuilder.DropTable(
                name: "MenusAttributesConfig");

            migrationBuilder.DropTable(
                name: "StatusConfig");

            migrationBuilder.DropTable(
                name: "UserColumnView");
        }
    }
}
