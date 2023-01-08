using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibrary.Infrastructure.Migrations
{
    public partial class DeveloperGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeveloperGame",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperGame", x => new { x.UserId, x.GameId });
                    table.ForeignKey(
                        name: "FK_DeveloperGame_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeveloperGame_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85601b02-9a83-47d0-b4a2-fcd5c6c16f1e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3be64854-0bb7-42ed-a4a9-2f7370ebb8aa", "AQAAAAEAACcQAAAAEOBfNs7dT9tQrsR1BkOWNF+FN89uuIwT3C9dUZ8l7OKpSMU+rVZIsAy33cxf+PJ/xA==", "7a33f77f-c317-401b-87d8-913d67713c8b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ee86a76-a953-42dc-b21f-58646a71fb49", "AQAAAAEAACcQAAAAEJ+pOipEggIKRwDd8ykNfCKuB+KEM58L/ZUm+pQt2UvaaYBsv56+OW9CDffgwXi69w==", "f1c974b2-e031-482c-b75b-f152e9f1be01" });

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperGame_GameId",
                table: "DeveloperGame",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeveloperGame");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "85601b02-9a83-47d0-b4a2-fcd5c6c16f1e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2028758-c3a5-4a1e-8113-991cd4ce9ac7", "AQAAAAEAACcQAAAAEO/RKMUjviMrHjwvc1Cy/PzAjD+3Vn018bfWlwbehGTLD568Jkf+7ObJM9LGjnkmQA==", "ebba9db5-3e64-4303-8911-c24bb4ab9243" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0bc52b04-d099-4b8b-92e3-ff7b7cd70996", "AQAAAAEAACcQAAAAEEvwm1eG7mKTNFGeR4Pn60gZmWP82cWKNbZV3CFD2Je9Yyyzg01wny2t0C4b77mWfA==", "45ca9c6f-ff3f-4275-b628-0c1b178138cd" });
        }
    }
}
