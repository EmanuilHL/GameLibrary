using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibrary.Infrastructure.Migrations
{
    public partial class HelperAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Helpers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Helpers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Helpers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "465ea7c4-121c-4b54-bdce-3e5b60e99152", "AQAAAAEAACcQAAAAEPuZ8LbHmprEhZCrS2fw/JmFLgG9CFAvRtVJfzNXPZZbec6fA2XOwMH8Sof6TPq1FQ==", "7fb41c36-ff0e-4b73-98ad-45e91f69ec59" });

            migrationBuilder.CreateIndex(
                name: "IX_Helpers_UserId",
                table: "Helpers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Helpers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e8f8105-e929-4616-83de-28020aa662be", "AQAAAAEAACcQAAAAEMgkN1kWbdvqgkaIJQMLmZhNj+Sn03kotFIWidu8InmqSqzdYX45A1tZwufTrwV1sw==", "2917b318-2d07-4330-bfa8-492647f9aa5b" });
        }
    }
}
