using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.GitHubFinderAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddGitHubFinder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GitHubFinders",
                columns: table => new
                {
                    NameFinder = table.Column<string>(type: "text", nullable: false),
                    Repositories = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GitHubFinders", x => x.NameFinder);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GitHubFinders");
        }
    }
}
