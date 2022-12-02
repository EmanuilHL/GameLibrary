using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibrary.Infrastructure.Migrations
{
    public partial class GameMechanicAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3c1f6f1-846f-43e6-be9e-ebf6eddbba52", "AQAAAAEAACcQAAAAEIlfoWzcNiwbW7zJQKgddQ76azbvCOU6f5gvnfFmr5gOu/rdk5orrxJu/t7b417xjQ==", "f923870b-64a4-442c-9804-259cc4677245" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "465ea7c4-121c-4b54-bdce-3e5b60e99152", "AQAAAAEAACcQAAAAEPuZ8LbHmprEhZCrS2fw/JmFLgG9CFAvRtVJfzNXPZZbec6fA2XOwMH8Sof6TPq1FQ==", "7fb41c36-ff0e-4b73-98ad-45e91f69ec59" });
        }
    }
}
