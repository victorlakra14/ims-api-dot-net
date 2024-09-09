using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class Addedusercreationendpoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1436d0de-5851-4697-9c05-c688a50fa660", null, "Admin", "ADMIN" },
                    { "1df4c906-d8d1-4052-9937-418c1dfe047a", null, "Staff", "STAFF" },
                    { "99441ef5-8773-41cc-92e3-97be2d9ccfba", null, "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1436d0de-5851-4697-9c05-c688a50fa660");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1df4c906-d8d1-4052-9937-418c1dfe047a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99441ef5-8773-41cc-92e3-97be2d9ccfba");
        }
    }
}
