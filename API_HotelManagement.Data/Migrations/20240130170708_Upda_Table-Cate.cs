using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_HotelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class Upda_TableCate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ht_OrderRoomDetail");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ht_OrderRoomDetail");

            migrationBuilder.DropColumn(
                name: "TimeEnd",
                table: "ht_Order");

            migrationBuilder.DropColumn(
                name: "TimeStart",
                table: "ht_Order");

            migrationBuilder.RenameColumn(
                name: "OrderTime",
                table: "ht_OrderRoomDetail",
                newName: "TimeStart");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryServiceId",
                table: "ht_Service",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryRoomId",
                table: "ht_Room",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "ht_OrderServiceDetail",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "ht_OrderServiceDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeEnd",
                table: "ht_OrderRoomDetail",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "ht_Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ht_CategoryRoom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_CategoryRoom", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ht_CategoryService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_CategoryService", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ht_Service_CategoryServiceId",
                table: "ht_Service",
                column: "CategoryServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ht_Room_CategoryRoomId",
                table: "ht_Room",
                column: "CategoryRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_ht_Room_ht_CategoryRoom_CategoryRoomId",
                table: "ht_Room",
                column: "CategoryRoomId",
                principalTable: "ht_CategoryRoom",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ht_Service_ht_CategoryService_CategoryServiceId",
                table: "ht_Service",
                column: "CategoryServiceId",
                principalTable: "ht_CategoryService",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ht_Room_ht_CategoryRoom_CategoryRoomId",
                table: "ht_Room");

            migrationBuilder.DropForeignKey(
                name: "FK_ht_Service_ht_CategoryService_CategoryServiceId",
                table: "ht_Service");

            migrationBuilder.DropTable(
                name: "ht_CategoryRoom");

            migrationBuilder.DropTable(
                name: "ht_CategoryService");

            migrationBuilder.DropIndex(
                name: "IX_ht_Service_CategoryServiceId",
                table: "ht_Service");

            migrationBuilder.DropIndex(
                name: "IX_ht_Room_CategoryRoomId",
                table: "ht_Room");

            migrationBuilder.DropColumn(
                name: "CategoryServiceId",
                table: "ht_Service");

            migrationBuilder.DropColumn(
                name: "CategoryRoomId",
                table: "ht_Room");

            migrationBuilder.DropColumn(
                name: "TimeEnd",
                table: "ht_OrderRoomDetail");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "ht_Order");

            migrationBuilder.RenameColumn(
                name: "TimeStart",
                table: "ht_OrderRoomDetail",
                newName: "OrderTime");

            migrationBuilder.AlterColumn<string>(
                name: "TotalPrice",
                table: "ht_OrderServiceDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "ht_OrderServiceDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "ht_OrderRoomDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalPrice",
                table: "ht_OrderRoomDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeEnd",
                table: "ht_Order",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStart",
                table: "ht_Order",
                type: "datetime2",
                nullable: true);
        }
    }
}
