using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmidogsManager.Database.Migrations
{
    public partial class Meetingchanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hora",
                table: "Meeting");

            migrationBuilder.RenameColumn(
                name: "Ubication",
                table: "Meeting",
                newName: "Location");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Meeting",
                newName: "Ubication");

            migrationBuilder.AddColumn<DateTime>(
                name: "Hora",
                table: "Meeting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
