using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibrary.Infrastructure.Migrations
{
    public partial class DSSC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Games_GameId",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Comment",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85601b02-9a83-47d0-b4a2-fcd5c6c16f1e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f5a4cacd-cf32-4c32-ae81-e3bba6133ea9", "AQAAAAEAACcQAAAAEJR8ZKG8Zg1YkrtB+ICkoOD7kCIL9Vs+za9u2u5XgreFTikb62iQwh7A+Hyf2wteQg==", "391a0bcb-d88a-4e8c-bfcd-6bc131182015" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5265b253-4abe-48fd-be26-059555a08518", "AQAAAAEAACcQAAAAEDdk3WMBwf56vN5zBrr7HppXzClO/m2pqJF1jBe1j2SNTtrvj3w5/Na4F0lPIAqfhA==", "596ea3ec-73a2-45e6-b991-04af9cff583c" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Games_GameId",
                table: "Comment",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Games_GameId",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Games_GameId",
                table: "Comment",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
