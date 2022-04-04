using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class SetupRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Event_EventId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_EventId",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Team");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Date",
                table: "Event",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Team",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Date",
                table: "Event",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.CreateIndex(
                name: "IX_Team_EventId",
                table: "Team",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Event_EventId",
                table: "Team",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
