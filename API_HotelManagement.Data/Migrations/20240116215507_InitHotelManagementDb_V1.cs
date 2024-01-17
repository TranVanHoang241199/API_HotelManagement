using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_HotelManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitHotelManagementDb_V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ht_Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ht_Test", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Ht_Test",
                columns: new[] { "Id", "CreateBy", "CreateDate", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("801f03d6-5bb6-44b1-9460-9cdf4617aaf9"), new Guid("7c67e72e-e773-447f-9f3e-9c3a480be605"), new DateTime(2024, 1, 17, 4, 55, 6, 939, DateTimeKind.Local).AddTicks(7949), new Guid("7c67e72e-e773-447f-9f3e-9c3a480be605"), new DateTime(2024, 1, 17, 4, 55, 6, 939, DateTimeKind.Local).AddTicks(7951), "hello 2" },
                    { new Guid("9d749d28-3405-45f7-8fd1-fdae05a0dea9"), new Guid("7c67e72e-e773-447f-9f3e-9c3a480be605"), new DateTime(2024, 1, 17, 4, 55, 6, 939, DateTimeKind.Local).AddTicks(7928), new Guid("7c67e72e-e773-447f-9f3e-9c3a480be605"), new DateTime(2024, 1, 17, 4, 55, 6, 939, DateTimeKind.Local).AddTicks(7947), "hello 1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ht_Test");
        }
    }
}
