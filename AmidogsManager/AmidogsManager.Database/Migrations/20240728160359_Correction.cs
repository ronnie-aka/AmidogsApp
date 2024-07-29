using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmidogsManager.Database.Migrations
{
    public partial class Correction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Dog_Dog1Id",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Dog_Dog2Id",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_Dog1Id",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_Dog2Id",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Dog1Id",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Dog2Id",
                table: "Match");

            migrationBuilder.CreateIndex(
                name: "IX_Match_DogId1",
                table: "Match",
                column: "DogId1");

            migrationBuilder.CreateIndex(
                name: "IX_Match_DogId2",
                table: "Match",
                column: "DogId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Dog_DogId1",
                table: "Match",
                column: "DogId1",
                principalTable: "Dog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Dog_DogId2",
                table: "Match",
                column: "DogId2",
                principalTable: "Dog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Dog_DogId1",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Dog_DogId2",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_DogId1",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_DogId2",
                table: "Match");

            migrationBuilder.AddColumn<int>(
                name: "Dog1Id",
                table: "Match",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Dog2Id",
                table: "Match",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Match_Dog1Id",
                table: "Match",
                column: "Dog1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Match_Dog2Id",
                table: "Match",
                column: "Dog2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Dog_Dog1Id",
                table: "Match",
                column: "Dog1Id",
                principalTable: "Dog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Dog_Dog2Id",
                table: "Match",
                column: "Dog2Id",
                principalTable: "Dog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
