using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WORKMAN.Config.Migrations
{
    /// <inheritdoc />
    public partial class addissystemMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCreatedBySystem",
                table: "MenuConfig",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCreatedBySystem",
                table: "MenuConfig");
        }
    }
}
