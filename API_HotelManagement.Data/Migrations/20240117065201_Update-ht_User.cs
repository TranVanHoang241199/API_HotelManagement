using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_HotelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updateht_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ht_Test",
                table: "Ht_Test");

            migrationBuilder.RenameTable(
                name: "Ht_Test",
                newName: "ht_Test");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ht_Test",
                table: "ht_Test",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ht_Test",
                table: "ht_Test");

            migrationBuilder.RenameTable(
                name: "ht_Test",
                newName: "Ht_Test");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ht_Test",
                table: "Ht_Test",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Ht_Test",
                keyColumn: "Id",
                keyValue: new Guid("801f03d6-5bb6-44b1-9460-9cdf4617aaf9"),
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 1, 17, 4, 55, 6, 939, DateTimeKind.Local).AddTicks(7949), new DateTime(2024, 1, 17, 4, 55, 6, 939, DateTimeKind.Local).AddTicks(7951) });

            migrationBuilder.UpdateData(
                table: "Ht_Test",
                keyColumn: "Id",
                keyValue: new Guid("9d749d28-3405-45f7-8fd1-fdae05a0dea9"),
                columns: new[] { "CreateDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 1, 17, 4, 55, 6, 939, DateTimeKind.Local).AddTicks(7928), new DateTime(2024, 1, 17, 4, 55, 6, 939, DateTimeKind.Local).AddTicks(7947) });
        }
    }
}
