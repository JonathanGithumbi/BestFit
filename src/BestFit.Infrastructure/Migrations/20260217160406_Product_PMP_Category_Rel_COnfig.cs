using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Product_PMP_Category_Rel_COnfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Categories_CategoriesId",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductMeasurementProfiles_ProductMeasurementProfileId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductMeasurementProfileId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductMeasurementProfileId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "CategoryProduct",
                newName: "CategoryId");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "ProductMeasurementProfiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProductMeasurementProfiles_ProductId",
                table: "ProductMeasurementProfiles",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Categories_CategoryId",
                table: "CategoryProduct",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMeasurementProfiles_Products_ProductId",
                table: "ProductMeasurementProfiles",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Categories_CategoryId",
                table: "CategoryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductMeasurementProfiles_Products_ProductId",
                table: "ProductMeasurementProfiles");

            migrationBuilder.DropIndex(
                name: "IX_ProductMeasurementProfiles_ProductId",
                table: "ProductMeasurementProfiles");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductMeasurementProfiles");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "CategoryProduct",
                newName: "CategoriesId");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductMeasurementProfileId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductMeasurementProfileId",
                table: "Products",
                column: "ProductMeasurementProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Categories_CategoriesId",
                table: "CategoryProduct",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductMeasurementProfiles_ProductMeasurementProfileId",
                table: "Products",
                column: "ProductMeasurementProfileId",
                principalTable: "ProductMeasurementProfiles",
                principalColumn: "Id");
        }
    }
}
