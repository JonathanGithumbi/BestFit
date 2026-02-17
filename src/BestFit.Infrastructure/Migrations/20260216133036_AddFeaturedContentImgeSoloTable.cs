using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFeaturedContentImgeSoloTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeaturedContents_Images_ImageId",
                table: "FeaturedContents");

            migrationBuilder.DropIndex(
                name: "IX_FeaturedContents_ImageId",
                table: "FeaturedContents");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "FeaturedContents");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "FeaturedContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FeaturedContentImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeaturedContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturedContentImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeaturedContentImage_FeaturedContents_FeaturedContentId",
                        column: x => x.FeaturedContentId,
                        principalTable: "FeaturedContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeaturedContentImage_FeaturedContentId",
                table: "FeaturedContentImage",
                column: "FeaturedContentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeaturedContentImage");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "FeaturedContents");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Images",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "FeaturedContents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
