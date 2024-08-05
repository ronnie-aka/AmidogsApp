using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmidogsManager.Database.Migrations
{
    public partial class Modificationinclases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MeetingName",
                table: "Meeting",
                newName: "MeetingTitle");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Dog",
                newName: "Sterilized");

            migrationBuilder.RenameColumn(
                name: "Castrated",
                table: "Dog",
                newName: "Sex");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MeetingTitle",
                table: "Meeting",
                newName: "MeetingName");

            migrationBuilder.RenameColumn(
                name: "Sterilized",
                table: "Dog",
                newName: "Genre");

            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "Dog",
                newName: "Castrated");
        }
    }
}
