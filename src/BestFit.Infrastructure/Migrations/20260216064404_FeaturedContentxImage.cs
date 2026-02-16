using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FeaturedContentxImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeaturedContents_Images_Id",
                table: "FeaturedContents");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturedContents_ImageId",
                table: "FeaturedContents",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeaturedContents_Images_ImageId",
                table: "FeaturedContents",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeaturedContents_Images_ImageId",
                table: "FeaturedContents");

            migrationBuilder.DropIndex(
                name: "IX_FeaturedContents_ImageId",
                table: "FeaturedContents");

            migrationBuilder.AddForeignKey(
                name: "FK_FeaturedContents_Images_Id",
                table: "FeaturedContents",
                column: "Id",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
