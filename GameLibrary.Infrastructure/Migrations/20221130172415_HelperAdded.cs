using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibrary.Infrastructure.Migrations
{
    public partial class HelperAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e8f8105-e929-4616-83de-28020aa662be", "AQAAAAEAACcQAAAAEMgkN1kWbdvqgkaIJQMLmZhNj+Sn03kotFIWidu8InmqSqzdYX45A1tZwufTrwV1sw==", "2917b318-2d07-4330-bfa8-492647f9aa5b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "432f137a-cba3-4d5d-a8e9-9e42d17a0d4d", "AQAAAAEAACcQAAAAECQbiYwUQyGpcxAwUBzUoiWdHI0FhFQN6ttWVxJ0J6YWLYeUU6+3Zxj9s+lEZQ3YDw==", "57acd8c3-8bef-4528-a624-6bf8c3dd4244" });
        }
    }
}
