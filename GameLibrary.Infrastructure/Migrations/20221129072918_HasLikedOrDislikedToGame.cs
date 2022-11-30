using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibrary.Infrastructure.Migrations
{
    public partial class HasLikedOrDislikedToGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasDisliked",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HasLiked",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "HasDisliked",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasLiked",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8a73404-a0dc-4456-8026-4a3f5670d0df", "AQAAAAEAACcQAAAAEGuw4aS9VgEpPkVcCbwc7RjeXTVOzRC2Q0KhRv04SAMe0jI8OFoUG3HUu0c5RopV2g==", "653adbc7-21b5-4352-8b92-f53e547df366" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasDisliked",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "HasLiked",
                table: "Games");

            migrationBuilder.AddColumn<bool>(
                name: "HasDisliked",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasLiked",
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
    }
}
