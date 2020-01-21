using Microsoft.EntityFrameworkCore.Migrations;

namespace FarmApp.Infrastructure.Data.Migrations
{
    public partial class Add_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dist",
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { 1, "admin" });

            migrationBuilder.InsertData(
                schema: "dist",
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { 2, "user" });

            migrationBuilder.InsertData(
                schema: "dist",
                table: "Users",
                columns: new[] { "Id", "Login", "Password", "RoleId", "UserName" },
                values: new object[] { 1, "admin", "123456", 1, "Админ" });

            migrationBuilder.InsertData(
                schema: "dist",
                table: "Users",
                columns: new[] { "Id", "Login", "Password", "RoleId", "UserName" },
                values: new object[] { 2, "user", "123456", 2, "Пользователь" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dist",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dist",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "dist",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dist",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
