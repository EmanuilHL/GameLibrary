using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibrary.Infrastructure.Migrations
{
    public partial class UserHasAbilityToLikeOrDislike2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasLikedOrDisliked",
                table: "AspNetUsers",
                newName: "HasLiked");

            migrationBuilder.AddColumn<bool>(
                name: "HasDisliked",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80ef270e-5eb9-4687-b15e-92fa7dfe13cd", "AQAAAAEAACcQAAAAEEuBqgo0311+2acq6OMQpaqQON9lv3CluBS4Lv2mZj2hjvNF88cz+CFU6GTkZAzTNQ==", "3e3eb850-5a18-4724-9240-58fd70730301" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasDisliked",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "HasLiked",
                table: "AspNetUsers",
                newName: "HasLikedOrDisliked");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "efa5fe67-64d0-4119-8b7e-b4310d1bbb21", "AQAAAAEAACcQAAAAEJbJSNlIamMCyrKwjJUiKlbNeSx78UA2JRRnBfGEFX9F7Pvtj2nd/vfURqA0RWv+4w==", "04ed10aa-4a27-4ec6-916a-4a23d9e92888" });
        }
    }
}
