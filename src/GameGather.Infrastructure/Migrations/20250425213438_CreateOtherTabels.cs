using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameGather.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateOtherTabels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SessionGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    GameMasterId = table.Column<int>(type: "integer", nullable: false),
                    GameMasterName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionGames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SessionGameId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    DateComment = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_SessionGames_SessionGameId",
                        column: x => x.SessionGameId,
                        principalTable: "SessionGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameMasterId = table.Column<int>(type: "integer", nullable: false),
                    SessionGameId = table.Column<int>(type: "integer", nullable: false),
                    DayPost = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GameTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostGames_SessionGames_SessionGameId",
                        column: x => x.SessionGameId,
                        principalTable: "SessionGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionGameLists",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SessionGameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionGameLists", x => new { x.UserId, x.SessionGameId });
                    table.ForeignKey(
                        name: "FK_SessionGameLists_SessionGames_SessionGameId",
                        column: x => x.SessionGameId,
                        principalTable: "SessionGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionGameLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SessionGameId",
                table: "Comments",
                column: "SessionGameId");

            migrationBuilder.CreateIndex(
                name: "IX_PostGames_SessionGameId",
                table: "PostGames",
                column: "SessionGameId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionGameLists_SessionGameId",
                table: "SessionGameLists",
                column: "SessionGameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "PostGames");

            migrationBuilder.DropTable(
                name: "SessionGameLists");

            migrationBuilder.DropTable(
                name: "SessionGames");
        }
    }
}
