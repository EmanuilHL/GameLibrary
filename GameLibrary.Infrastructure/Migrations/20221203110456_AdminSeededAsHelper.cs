using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibrary.Infrastructure.Migrations
{
    public partial class AdminSeededAsHelper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85601b02-9a83-47d0-b4a2-fcd5c6c16f1e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "879dd0e3-42c4-477e-a4db-2687156d277b", "AQAAAAEAACcQAAAAEMZVm6X6hrSbLjpsK2c2DnMWA0l8E/q0nP7dE5n25R55Qo0wfFGB8r4+OvZ/ISJWFQ==", "721b9877-5d18-455e-8c65-c64dd3d56d82" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4479dad0-55dc-4161-8b08-b3c530d729ae", "AQAAAAEAACcQAAAAEHu6SKWAQ9NmF2fFJeEWJpC32ScvReoRFBqGkr6yH14z/g3yD0M5+h4wjOC+Q107fA==", "c820215a-4908-4d15-bb8b-eae6dd36a4a8" });

            migrationBuilder.InsertData(
                table: "Helpers",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[] { 3, "+359134554324", "85601b02-9a83-47d0-b4a2-fcd5c6c16f1e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Helpers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85601b02-9a83-47d0-b4a2-fcd5c6c16f1e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d79ca62-d41d-4dec-987c-88d43dd4d248", "AQAAAAEAACcQAAAAEIXdBU0dcsFYUjNBRys79BwlY+cxy99eNoAvE4o/qtHyWbzjqVvepmzy+E7P7TxZbQ==", "eb95f97d-8b93-4754-a855-268b307c7bef" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "235cbfc6-c265-47b9-879c-06ebd8eea827", "AQAAAAEAACcQAAAAEEgeWQ7ShfxkIAtY9dd88GmIpHD+YaCozpC4cCtozhLQROq20sZQZhHY9Zw3iC/isg==", "94444fb9-7a51-4aac-b981-4994a2faf7fc" });
        }
    }
}
