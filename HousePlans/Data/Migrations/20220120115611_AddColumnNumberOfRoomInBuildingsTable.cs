using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousePlans.Data.Migrations
{
    public partial class AddColumnNumberOfRoomInBuildingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfRoom",
                table: "Buildings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfRoom",
                table: "Buildings");
        }
    }
}
