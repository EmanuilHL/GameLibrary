using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibrary.Infrastructure.Migrations
{
    public partial class GameMechanicAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameMechanics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MechanicDescription = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HelperId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameMechanics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameMechanics_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameMechanics_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameMechanics_Helpers_HelperId",
                        column: x => x.HelperId,
                        principalTable: "Helpers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "123c1e03-3bf5-4c13-8797-dc71da37b25b", "AQAAAAEAACcQAAAAEIOXVKfSdbBvIiWMpllOKiKo+57GO1/vAjAG2YlcA3v49DyHr0+mM8NdbFATAxMFow==", "ccf5870d-665e-479a-a329-712c65acd7d0" });

            migrationBuilder.CreateIndex(
                name: "IX_GameMechanics_GameId",
                table: "GameMechanics",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameMechanics_HelperId",
                table: "GameMechanics",
                column: "HelperId");

            migrationBuilder.CreateIndex(
                name: "IX_GameMechanics_UserId",
                table: "GameMechanics",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameMechanics");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3c1f6f1-846f-43e6-be9e-ebf6eddbba52", "AQAAAAEAACcQAAAAEIlfoWzcNiwbW7zJQKgddQ76azbvCOU6f5gvnfFmr5gOu/rdk5orrxJu/t7b417xjQ==", "f923870b-64a4-442c-9804-259cc4677245" });
        }
    }
}
