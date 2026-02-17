using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePictireToImageUrlFeaturedContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "FeaturedContents",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "FeaturedContents",
                newName: "Picture");
        }
    }
}
