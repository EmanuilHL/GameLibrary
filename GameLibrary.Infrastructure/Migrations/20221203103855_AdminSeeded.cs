using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibrary.Infrastructure.Migrations
{
    public partial class AdminSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "235cbfc6-c265-47b9-879c-06ebd8eea827", "AQAAAAEAACcQAAAAEEgeWQ7ShfxkIAtY9dd88GmIpHD+YaCozpC4cCtozhLQROq20sZQZhHY9Zw3iC/isg==", "94444fb9-7a51-4aac-b981-4994a2faf7fc" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "85601b02-9a83-47d0-b4a2-fcd5c6c16f1e", 0, "9d79ca62-d41d-4dec-987c-88d43dd4d248", "admin@mail.com", false, false, null, "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAEAACcQAAAAEIXdBU0dcsFYUjNBRys79BwlY+cxy99eNoAvE4o/qtHyWbzjqVvepmzy+E7P7TxZbQ==", null, false, "eb95f97d-8b93-4754-a855-268b307c7bef", false, "admin@mail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85601b02-9a83-47d0-b4a2-fcd5c6c16f1e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1f3cd1d-25b2-4be7-a03f-a4e4425b2812", "AQAAAAEAACcQAAAAEIfaDANAX90ob+0IBKcgyYlTx2eXgYFcuJekD1StQRQScHLwTloGRmm1UOkvGDU1cQ==", "3cebdfe7-6b39-49d0-9f41-b2453d79bcee" });
        }
    }
}
