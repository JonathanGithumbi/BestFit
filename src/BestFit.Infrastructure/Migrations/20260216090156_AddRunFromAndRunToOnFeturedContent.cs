using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRunFromAndRunToOnFeturedContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "FeaturedContents",
                newName: "SubHeading");

            migrationBuilder.AddColumn<string>(
                name: "Heading",
                table: "FeaturedContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RunFromDate",
                table: "FeaturedContents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RunToDate",
                table: "FeaturedContents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Heading",
                table: "FeaturedContents");

            migrationBuilder.DropColumn(
                name: "RunFromDate",
                table: "FeaturedContents");

            migrationBuilder.DropColumn(
                name: "RunToDate",
                table: "FeaturedContents");

            migrationBuilder.RenameColumn(
                name: "SubHeading",
                table: "FeaturedContents",
                newName: "Message");
        }
    }
}
