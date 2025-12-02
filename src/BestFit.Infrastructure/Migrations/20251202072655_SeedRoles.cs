using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BestFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "38c6732b-f36b-4e3a-b8f6-26be2dce3973", "38c6732b-f36b-4e3a-b8f6-26be2dce3973", "Administrator", "ADMINISTRATOR" },
                    { "3c7b2bba-99ab-4768-b124-4abb40a94daa", "3c7b2bba-99ab-4768-b124-4abb40a94daa", "Handler", "HANDLER" },
                    { "6afa6e31-910f-4963-94ac-d7a23c6d377d", "6afa6e31-910f-4963-94ac-d7a23c6d377d", "Shopper", "SHOPPER" },
                    { "cfab018d-40e3-4f0c-af89-00e7cd9f9724", "cfab018d-40e3-4f0c-af89-00e7cd9f9724", "Staff", "STAFF" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38c6732b-f36b-4e3a-b8f6-26be2dce3973");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c7b2bba-99ab-4768-b124-4abb40a94daa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6afa6e31-910f-4963-94ac-d7a23c6d377d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cfab018d-40e3-4f0c-af89-00e7cd9f9724");
        }
    }
}
