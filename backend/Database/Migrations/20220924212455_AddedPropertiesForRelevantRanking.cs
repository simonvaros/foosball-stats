using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddedPropertiesForRelevantRanking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayedRelevantEvents",
                table: "Player",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "RelevantFirstPlacePercentage",
                table: "Player",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "RelevantFirstPlacesCount",
                table: "Player",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "RelevantSecondPlacePercentage",
                table: "Player",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "RelevantSecondPlacesCount",
                table: "Player",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "RelevantThirdPlacePercentage",
                table: "Player",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "RelevantThirdPlacesCount",
                table: "Player",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "RelevantTop3Percentage",
                table: "Player",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "RelevantForRanking",
                table: "Event",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayedRelevantEvents",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "RelevantFirstPlacePercentage",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "RelevantFirstPlacesCount",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "RelevantSecondPlacePercentage",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "RelevantSecondPlacesCount",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "RelevantThirdPlacePercentage",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "RelevantThirdPlacesCount",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "RelevantTop3Percentage",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "RelevantForRanking",
                table: "Event");
        }
    }
}
