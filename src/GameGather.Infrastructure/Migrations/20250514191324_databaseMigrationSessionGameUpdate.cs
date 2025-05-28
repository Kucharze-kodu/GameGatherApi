using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameGather.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class databaseMigrationSessionGameUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SessionGames",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SessionGames");
        }
    }
}
