using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    public partial class changeWinner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Winner",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Winner_UserId",
                table: "Winner",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Winner_AspNetUsers_UserId",
                table: "Winner",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Winner_AspNetUsers_UserId",
                table: "Winner");

            migrationBuilder.DropIndex(
                name: "IX_Winner_UserId",
                table: "Winner");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Winner",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
