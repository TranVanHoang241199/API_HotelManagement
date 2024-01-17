using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_HotelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updateht_Userl2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ht_User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ht_User", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "ht_Test",
                keyColumn: "Id",
                keyValue: new Guid("801f03d6-5bb6-44b1-9460-9cdf4617aaf9"),
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 1, 17, 13, 54, 15, 754, DateTimeKind.Local).AddTicks(8840), new DateTime(2024, 1, 17, 13, 54, 15, 754, DateTimeKind.Local).AddTicks(8841) });

            migrationBuilder.UpdateData(
                table: "ht_Test",
                keyColumn: "Id",
                keyValue: new Guid("9d749d28-3405-45f7-8fd1-fdae05a0dea9"),
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 1, 17, 13, 54, 15, 754, DateTimeKind.Local).AddTicks(8816), new DateTime(2024, 1, 17, 13, 54, 15, 754, DateTimeKind.Local).AddTicks(8837) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ht_User");

            migrationBuilder.UpdateData(
                table: "ht_Test",
                keyColumn: "Id",
                keyValue: new Guid("801f03d6-5bb6-44b1-9460-9cdf4617aaf9"),
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 1, 17, 13, 52, 1, 740, DateTimeKind.Local).AddTicks(9449), new DateTime(2024, 1, 17, 13, 52, 1, 740, DateTimeKind.Local).AddTicks(9450) });

            migrationBuilder.UpdateData(
                table: "ht_Test",
                keyColumn: "Id",
                keyValue: new Guid("9d749d28-3405-45f7-8fd1-fdae05a0dea9"),
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 1, 17, 13, 52, 1, 740, DateTimeKind.Local).AddTicks(9423), new DateTime(2024, 1, 17, 13, 52, 1, 740, DateTimeKind.Local).AddTicks(9445) });
        }
    }
}
