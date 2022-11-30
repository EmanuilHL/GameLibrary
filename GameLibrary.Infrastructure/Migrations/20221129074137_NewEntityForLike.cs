using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibrary.Infrastructure.Migrations
{
    public partial class NewEntityForLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserGameForLike",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGameForLike", x => new { x.UserId, x.GameId });
                    table.ForeignKey(
                        name: "FK_UserGameForLike_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGameForLike_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "432f137a-cba3-4d5d-a8e9-9e42d17a0d4d", "AQAAAAEAACcQAAAAECQbiYwUQyGpcxAwUBzUoiWdHI0FhFQN6ttWVxJ0J6YWLYeUU6+3Zxj9s+lEZQ3YDw==", "57acd8c3-8bef-4528-a624-6bf8c3dd4244" });

            migrationBuilder.CreateIndex(
                name: "IX_UserGameForLike_GameId",
                table: "UserGameForLike",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGameForLike");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8a73404-a0dc-4456-8026-4a3f5670d0df", "AQAAAAEAACcQAAAAEGuw4aS9VgEpPkVcCbwc7RjeXTVOzRC2Q0KhRv04SAMe0jI8OFoUG3HUu0c5RopV2g==", "653adbc7-21b5-4352-8b92-f53e547df366" });
        }
    }
}
