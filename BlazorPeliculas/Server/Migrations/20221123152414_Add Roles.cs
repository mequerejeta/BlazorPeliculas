using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorPeliculas.Server.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "813b59bc-c451-4b31-8d66-32fbe88edb41", "9ebfb4f6-77a9-4e41-ace7-2bcf51ee8dd4", "admin", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "813b59bc-c451-4b31-8d66-32fbe88edb41");
        }
    }
}
