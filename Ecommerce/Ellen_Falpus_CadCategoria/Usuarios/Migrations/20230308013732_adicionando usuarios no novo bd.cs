using Microsoft.EntityFrameworkCore.Migrations;

namespace Usuarios.Migrations
{
    public partial class adicionandousuariosnonovobd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "4355bfc8-80d8-4f0b-b1fc-e22ed8f43aa6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998,
                column: "ConcurrencyStamp",
                value: "a3729814-5e26-45cf-bcb1-e0e81ad3c276");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "8f2da82b-9089-44d1-91a0-072d58c4b2e1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "03767b8d-5fca-490e-af54-97a7bd255757", "AQAAAAEAACcQAAAAEBIl9xgX22uNkD2r7WNXy0HmF3V/xle/zkYlNVPRrqqWeTx9jQJP5RSPk5L55QpGAw==", "4379adff-83fa-4e4f-a079-750b4ce382da" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "b2a24afb-26b8-4712-a1b2-2df57d2eb9b0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998,
                column: "ConcurrencyStamp",
                value: "e85fe93c-29bc-4aeb-bf14-18ea06b12de1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "1909b35a-5fd3-4df5-887b-2c63026af2bb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "841f5576-02c8-4618-885f-e763b8515bfe", "AQAAAAEAACcQAAAAEGhnjM+MNq80274g7SDYL7owUEHsXdY3pDWjuPOM8mxSYbX/eb+wDcI8Lk42qeYJ9Q==", "9dec7767-c7c3-4ab5-ba70-ae4e0e71386c" });
        }
    }
}
