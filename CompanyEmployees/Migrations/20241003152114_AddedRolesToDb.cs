using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyEmployees.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c477022d-0ef0-45e6-bc4d-a7329706a18e", "b796aed2-3469-477d-ae0e-5eba54aabbae", "Administrator", "ADMINISTRATOR" },
                    { "f0aa3f39-80b8-4dd1-996e-718cfdd9ad02", "a3cca9c5-89b3-4080-96b0-5cb68fd1f742", "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c477022d-0ef0-45e6-bc4d-a7329706a18e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0aa3f39-80b8-4dd1-996e-718cfdd9ad02");
        }
    }
}
