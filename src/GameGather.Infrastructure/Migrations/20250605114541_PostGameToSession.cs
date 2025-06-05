using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameGather.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PostGameToSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "PostGames");

            migrationBuilder.AddColumn<string>(
                name: "PostDescription",
                table: "PostGames",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "PostDescription",
                table: "PostGames");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "PostGames",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
