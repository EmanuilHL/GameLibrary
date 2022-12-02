using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibrary.Infrastructure.Migrations
{
    public partial class HelperFromGameMRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameMechanics_Helpers_HelperId",
                table: "GameMechanics");

            migrationBuilder.DropIndex(
                name: "IX_GameMechanics_HelperId",
                table: "GameMechanics");

            migrationBuilder.DropColumn(
                name: "HelperId",
                table: "GameMechanics");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1f3cd1d-25b2-4be7-a03f-a4e4425b2812", "AQAAAAEAACcQAAAAEIfaDANAX90ob+0IBKcgyYlTx2eXgYFcuJekD1StQRQScHLwTloGRmm1UOkvGDU1cQ==", "3cebdfe7-6b39-49d0-9f41-b2453d79bcee" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HelperId",
                table: "GameMechanics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "123c1e03-3bf5-4c13-8797-dc71da37b25b", "AQAAAAEAACcQAAAAEIOXVKfSdbBvIiWMpllOKiKo+57GO1/vAjAG2YlcA3v49DyHr0+mM8NdbFATAxMFow==", "ccf5870d-665e-479a-a329-712c65acd7d0" });

            migrationBuilder.CreateIndex(
                name: "IX_GameMechanics_HelperId",
                table: "GameMechanics",
                column: "HelperId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameMechanics_Helpers_HelperId",
                table: "GameMechanics",
                column: "HelperId",
                principalTable: "Helpers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
