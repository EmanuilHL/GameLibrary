using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibrary.Infrastructure.Migrations
{
    public partial class DeveloperIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeveloperId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "AspNetUsers");

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
        }
    }
}
