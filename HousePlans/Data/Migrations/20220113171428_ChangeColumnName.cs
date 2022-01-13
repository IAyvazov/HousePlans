using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousePlans.Data.Migrations
{
    public partial class ChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuiltUpArea",
                table: "Houses",
                newName: "BuildUpArea");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuildUpArea",
                table: "Houses",
                newName: "BuiltUpArea");
        }
    }
}
