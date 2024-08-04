using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addnotesinuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Notes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CreatedById",
                table: "Notes",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_CreatedById",
                table: "Notes",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_CreatedById",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_CreatedById",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Notes");
        }
    }
}
