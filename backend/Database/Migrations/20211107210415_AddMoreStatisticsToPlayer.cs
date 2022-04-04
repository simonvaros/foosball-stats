using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddMoreStatisticsToPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "FirstPlacePercentage",
                table: "Player",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SecondPlacePercentage",
                table: "Player",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ThirdPlacePercentage",
                table: "Player",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Top3Percentage",
                table: "Player",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "YearOfFirstEvent",
                table: "Player",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstPlacePercentage",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "SecondPlacePercentage",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "ThirdPlacePercentage",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "Top3Percentage",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "YearOfFirstEvent",
                table: "Player");
        }
    }
}
