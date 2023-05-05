using Microsoft.EntityFrameworkCore.Migrations;

namespace Usuarios.Migrations
{
    public partial class Atualizaçãoderoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "6d3839ad-f463-4042-b627-867dd479639d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99997, "5d53a66b-8fe9-4084-b2e4-b7413b8c5513", "regular", "REGULAR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c3d8ce0-3e0e-45d2-a3ac-f59068db3328", "AQAAAAEAACcQAAAAEPnRnhMIuktubacCl72Sn8i6ayC+0ZjLcTuBlCPTrlZVow3RVH3uT9Os/DamNWbEAg==", "c183f5ee-26a3-4515-8204-4aeb0de71a7a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "fbf3ad9b-a83d-40b4-a953-02de1caf9c7e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ffd9df1-f1ac-4b81-8624-aa86fa1e74ea", "AQAAAAEAACcQAAAAEOhZ2YrzEbuWvVAIctfkvda09b8gr6orZhsIG+mQ3xqvQIujuHDEgnt9VY2WdFK9eA==", "3a4ab041-0f82-4ecf-8380-71e109d091db" });
        }
    }
}
