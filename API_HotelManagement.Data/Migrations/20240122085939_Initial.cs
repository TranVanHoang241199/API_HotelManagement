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
                name: "ht_Room",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FloorNumber = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_Room", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ht_Service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameService = table.Column<string>(type: "text", nullable: true),
                    Money = table.Column<double>(type: "double precision", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_Service", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ht_Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BusinessAreas = table.Column<string>(type: "varchar(50)", nullable: false),
                    PasswordUpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ht_Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerPhone = table.Column<string>(type: "text", nullable: true),
                    CustomerName = table.Column<string>(type: "text", nullable: true),
                    TimeStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TimeEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Note = table.Column<string>(type: "Text", nullable: true),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ht_Order_ht_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "ht_Room",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ht_OrderDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ht_OrderDetail_ht_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "ht_Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ht_OrderDetail_ht_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ht_Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ht_Order_RoomId",
                table: "ht_Order",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ht_OrderDetail_OrderId",
                table: "ht_OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ht_OrderDetail_ServiceId",
                table: "ht_OrderDetail",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ht_OrderDetail");

            migrationBuilder.DropTable(
                name: "ht_Users");

            migrationBuilder.DropTable(
                name: "ht_Order");

            migrationBuilder.DropTable(
                name: "ht_Service");

            migrationBuilder.DropTable(
                name: "ht_Room");
        }
    }
}
