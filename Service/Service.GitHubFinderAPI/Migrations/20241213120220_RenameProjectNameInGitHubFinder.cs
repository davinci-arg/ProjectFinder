using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.GitHubFinderAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameProjectNameInGitHubFinder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameFinder",
                table: "GitHubFinders",
                newName: "ProjectName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectName",
                table: "GitHubFinders",
                newName: "NameFinder");
        }
    }
}
