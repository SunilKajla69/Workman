using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WORKMAN.UserManagement.Migrations
{
    /// <inheritdoc />
    public partial class ConvertToLongIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // This migration converts GUID IDs to BIGINT (long) for better scalability
            // Only execute for PostgreSQL
            if (migrationBuilder.ActiveProvider == "Npgsql.EntityFrameworkCore.PostgreSQL")
            {
                // Drop existing table and recreate with proper BIGSERIAL type
                migrationBuilder.DropTable(name: "UserProfiles");

                // Recreate UserProfiles table with BIGSERIAL ID
                migrationBuilder.CreateTable(
                    name: "UserProfiles",
                    columns: table => new
                    {
                        Id = table.Column<long>(type: "bigint", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                        FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                        LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                        PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                        IsActive = table.Column<bool>(type: "boolean", nullable: false),
                        CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    });

                // Recreate index
                migrationBuilder.CreateIndex(
                    name: "IX_UserProfiles_Email",
                    table: "UserProfiles",
                    column: "Email",
                    unique: true);
            }
            // For SQLite, recreate table
            else
            {
                migrationBuilder.DropTable(name: "UserProfiles");

                migrationBuilder.CreateTable(
                    name: "UserProfiles",
                    columns: table => new
                    {
                        Id = table.Column<long>(type: "INTEGER", nullable: false)
                            .Annotation("Sqlite:Autoincrement", true),
                        Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                        FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                        LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                        PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                        CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                        UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    });

                migrationBuilder.CreateIndex(
                    name: "IX_UserProfiles_Email",
                    table: "UserProfiles",
                    column: "Email",
                    unique: true);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert back to GUID/TEXT based IDs
            migrationBuilder.DropTable(name: "UserProfiles");
            // Recreate with original GUID structure
        }
    }
}
