using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibrary.Infrastructure.Migrations
{
    public partial class LikeAndDislike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Games_GameId",
                table: "Comment");

            migrationBuilder.AddColumn<int>(
                name: "DislikesCount",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LikesCount",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9bcb8fd-21c1-4e32-ae0f-fd4f16b801f9", "AQAAAAEAACcQAAAAEPofQdfwFfdOE9vDfJYQ6XYbaSjU+DMvd45UV3A+8vXzNNHghNhREU5BpjiLH6Q3jQ==", "2fa8d019-2c5f-48ee-b4ae-ad6fc1fd6b1b" });

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

            migrationBuilder.DropColumn(
                name: "DislikesCount",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "LikesCount",
                table: "Games");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba82cb70-95cb-4cbe-a0ba-17f8783b92e9", "AQAAAAEAACcQAAAAEONwTO/2itq/MIp/xZozCyE/eWSPcYrNIlUneCgkbBFlYxq5ABJ19/aKUq11Gssleg==", "c6372a8c-9d31-4d4f-9337-37f523591a79" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Games_GameId",
                table: "Comment",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
