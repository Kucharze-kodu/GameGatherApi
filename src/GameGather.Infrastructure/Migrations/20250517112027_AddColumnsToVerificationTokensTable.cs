using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameGather.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToVerificationTokensTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastSendOnUtc",
                table: "VerificationTokens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateOnly>(
                name: "UsedOnUtc",
                table: "VerificationTokens",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSendOnUtc",
                table: "ResetPasswordTokens",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSendOnUtc",
                table: "VerificationTokens");

            migrationBuilder.DropColumn(
                name: "UsedOnUtc",
                table: "VerificationTokens");

            migrationBuilder.DropColumn(
                name: "LastSendOnUtc",
                table: "ResetPasswordTokens");
        }
    }
}
