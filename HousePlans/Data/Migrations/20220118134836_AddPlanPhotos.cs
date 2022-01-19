using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HousePlans.Data.Migrations
{
    public partial class AddPlanPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Buildings_HouseId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_HouseId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "Photos");

            migrationBuilder.AddColumn<int>(
                name: "BuildingId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_BuildingId",
                table: "Photos",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PlanId",
                table: "Photos",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Buildings_BuildingId",
                table: "Photos",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Plans_PlanId",
                table: "Photos",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Buildings_BuildingId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Plans_PlanId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_BuildingId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_PlanId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Photos");

            migrationBuilder.AddColumn<int>(
                name: "HouseId",
                table: "Photos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_HouseId",
                table: "Photos",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Buildings_HouseId",
                table: "Photos",
                column: "HouseId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
