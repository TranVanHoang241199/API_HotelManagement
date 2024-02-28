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
                name: "ht_Hotel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_Hotel", x => x.Id);
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
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PasswordUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ht_CategoryRoom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_CategoryRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ht_CategoryRoom_ht_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "ht_Hotel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ht_CategoryService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_CategoryService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ht_CategoryService_ht_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "ht_Hotel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ht_Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ht_Role_ht_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "ht_Hotel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ht_Role_ht_User_UserId",
                        column: x => x.UserId,
                        principalTable: "ht_User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ht_Room",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FloorNumber = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CategoryRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CategoryServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    TimeStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "IX_ht_CategoryRoom_HotelId",
                table: "ht_CategoryRoom",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_ht_CategoryService_HotelId",
                table: "ht_CategoryService",
                column: "HotelId");

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
                name: "IX_ht_Role_HotelId",
                table: "ht_Role",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_ht_Role_UserId",
                table: "ht_Role",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ht_Room_CategoryRoomId",
                table: "ht_Room",
                column: "CategoryRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ht_Service_CategoryServiceId",
                table: "ht_Service",
                column: "CategoryServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ht_OrderRoomDetail");

            migrationBuilder.DropTable(
                name: "ht_OrderServiceDetail");

            migrationBuilder.DropTable(
                name: "ht_Role");

            migrationBuilder.DropTable(
                name: "ht_Room");

            migrationBuilder.DropTable(
                name: "ht_Order");

            migrationBuilder.DropTable(
                name: "ht_Service");

            migrationBuilder.DropTable(
                name: "ht_User");

            migrationBuilder.DropTable(
                name: "ht_CategoryRoom");

            migrationBuilder.DropTable(
                name: "ht_CategoryService");

            migrationBuilder.DropTable(
                name: "ht_Hotel");
        }
    }
}
