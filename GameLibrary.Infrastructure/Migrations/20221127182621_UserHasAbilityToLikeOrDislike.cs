using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibrary.Infrastructure.Migrations
{
    public partial class UserHasAbilityToLikeOrDislike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasLikedOrDisliked",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "efa5fe67-64d0-4119-8b7e-b4310d1bbb21", "AQAAAAEAACcQAAAAEJbJSNlIamMCyrKwjJUiKlbNeSx78UA2JRRnBfGEFX9F7Pvtj2nd/vfURqA0RWv+4w==", "04ed10aa-4a27-4ec6-916a-4a23d9e92888" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasLikedOrDisliked",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9bcb8fd-21c1-4e32-ae0f-fd4f16b801f9", "AQAAAAEAACcQAAAAEPofQdfwFfdOE9vDfJYQ6XYbaSjU+DMvd45UV3A+8vXzNNHghNhREU5BpjiLH6Q3jQ==", "2fa8d019-2c5f-48ee-b4ae-ad6fc1fd6b1b" });
        }
    }
}
