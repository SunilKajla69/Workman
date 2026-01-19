using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WORKMAN.Auth.Migrations
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
                // Drop existing tables and recreate with proper BIGSERIAL types
                migrationBuilder.DropTable(name: "RefreshTokens");
                migrationBuilder.DropTable(name: "Users");

                // Recreate Users table with BIGSERIAL ID
                migrationBuilder.CreateTable(
                    name: "Users",
                    columns: table => new
                    {
                        Id = table.Column<long>(type: "bigint", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                        PasswordHash = table.Column<string>(type: "text", nullable: false),
                        CreatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Users", x => x.Id);
                    });

                // Recreate RefreshTokens table with BIGSERIAL IDs
                migrationBuilder.CreateTable(
                    name: "RefreshTokens",
                    columns: table => new
                    {
                        Id = table.Column<long>(type: "bigint", nullable: false)
                            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                        UserId = table.Column<long>(type: "bigint", nullable: false),
                        Token = table.Column<string>(type: "text", nullable: false),
                        ExpiresAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                        RevokedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                        table.ForeignKey(
                            name: "FK_RefreshTokens_Users_UserId",
                            column: x => x.UserId,
                            principalTable: "Users",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

                // Recreate indexes
                migrationBuilder.CreateIndex(
                    name: "IX_RefreshTokens_Token",
                    table: "RefreshTokens",
                    column: "Token",
                    unique: true);

                migrationBuilder.CreateIndex(
                    name: "IX_RefreshTokens_UserId",
                    table: "RefreshTokens",
                    column: "UserId");

                migrationBuilder.CreateIndex(
                    name: "IX_Users_Email",
                    table: "Users",
                    column: "Email",
                    unique: true);
            }
            // For SQLite, recreate tables (SQLite doesn't support ALTER COLUMN for type changes)
            else
            {
                migrationBuilder.DropTable(name: "RefreshTokens");
                migrationBuilder.DropTable(name: "Users");

                migrationBuilder.CreateTable(
                    name: "Users",
                    columns: table => new
                    {
                        Id = table.Column<long>(type: "INTEGER", nullable: false)
                            .Annotation("Sqlite:Autoincrement", true),
                        Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                        PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                        CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Users", x => x.Id);
                    });

                migrationBuilder.CreateTable(
                    name: "RefreshTokens",
                    columns: table => new
                    {
                        Id = table.Column<long>(type: "INTEGER", nullable: false)
                            .Annotation("Sqlite:Autoincrement", true),
                        UserId = table.Column<long>(type: "INTEGER", nullable: false),
                        Token = table.Column<string>(type: "TEXT", nullable: false),
                        ExpiresAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                        RevokedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                        table.ForeignKey(
                            name: "FK_RefreshTokens_Users_UserId",
                            column: x => x.UserId,
                            principalTable: "Users",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

                migrationBuilder.CreateIndex(
                    name: "IX_RefreshTokens_Token",
                    table: "RefreshTokens",
                    column: "Token",
                    unique: true);

                migrationBuilder.CreateIndex(
                    name: "IX_RefreshTokens_UserId",
                    table: "RefreshTokens",
                    column: "UserId");

                migrationBuilder.CreateIndex(
                    name: "IX_Users_Email",
                    table: "Users",
                    column: "Email",
                    unique: true);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert back to GUID/TEXT based IDs
            migrationBuilder.DropTable(name: "RefreshTokens");
            migrationBuilder.DropTable(name: "Users");

            // Recreate with original GUID structure
            // (Same as InitialAuthSchema migration)
        }
    }
}
