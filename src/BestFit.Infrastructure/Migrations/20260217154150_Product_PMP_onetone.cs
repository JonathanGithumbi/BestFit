using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Product_PMP_onetone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMeasurementProfiles_Products_Id",
                table: "ProductMeasurementProfiles");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductMeasurementProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "UnitSystem",
                table: "ProductMeasurementProfiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Fabric_Elasticity",
                table: "ProductMeasurementProfiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Fabric_DesignedFit",
                table: "ProductMeasurementProfiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductMeasurementProfileId",
                table: "Products",
                column: "ProductMeasurementProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductMeasurementProfiles_ProductMeasurementProfileId",
                table: "Products",
                column: "ProductMeasurementProfileId",
                principalTable: "ProductMeasurementProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductMeasurementProfiles_ProductMeasurementProfileId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductMeasurementProfileId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "UnitSystem",
                table: "ProductMeasurementProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Fabric_Elasticity",
                table: "ProductMeasurementProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Fabric_DesignedFit",
                table: "ProductMeasurementProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "ProductMeasurementProfiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMeasurementProfiles_Products_Id",
                table: "ProductMeasurementProfiles",
                column: "Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
