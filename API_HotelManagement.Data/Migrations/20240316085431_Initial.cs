using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_HotelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ht_CategoryRoom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_CategoryService", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ht_Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "Text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ht_User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ht_Room",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FloorNumber = table.Column<int>(type: "int", nullable: false),
                    PriceAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CategoryRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_Room", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ht_Room_ht_CategoryRoom_CategoryRoomId",
                        column: x => x.CategoryRoomId,
                        principalTable: "ht_CategoryRoom",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ht_Service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CategoryServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ht_Service_ht_CategoryService_CategoryServiceId",
                        column: x => x.CategoryServiceId,
                        principalTable: "ht_CategoryService",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ht_OrderRoomDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_OrderRoomDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ht_OrderRoomDetail_ht_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "ht_Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ht_OrderRoomDetail_ht_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "ht_Room",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ht_OrderServiceDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    TotalPriceAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_OrderServiceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ht_OrderServiceDetail_ht_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "ht_Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ht_OrderServiceDetail_ht_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ht_Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ht_OrderRoomDetail_OrderId",
                table: "ht_OrderRoomDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ht_OrderRoomDetail_RoomId",
                table: "ht_OrderRoomDetail",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ht_OrderServiceDetail_OrderId",
                table: "ht_OrderServiceDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ht_OrderServiceDetail_ServiceId",
                table: "ht_OrderServiceDetail",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ht_Room_CategoryRoomId",
                table: "ht_Room",
                column: "CategoryRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ht_Service_CategoryServiceId",
                table: "ht_Service",
                column: "CategoryServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ht_User_UserName",
                table: "ht_User",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ht_OrderRoomDetail");

            migrationBuilder.DropTable(
                name: "ht_OrderServiceDetail");

            migrationBuilder.DropTable(
                name: "ht_User");

            migrationBuilder.DropTable(
                name: "ht_Room");

            migrationBuilder.DropTable(
                name: "ht_Order");

            migrationBuilder.DropTable(
                name: "ht_Service");

            migrationBuilder.DropTable(
                name: "ht_CategoryRoom");

            migrationBuilder.DropTable(
                name: "ht_CategoryService");
        }
    }
}
