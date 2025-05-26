using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class _220525 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hours_Users_UserId",
                table: "Hours");

            migrationBuilder.DropIndex(
                name: "IX_Hours_UserId",
                table: "Hours");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Hours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Hours",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Hours_UserId",
                table: "Hours",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hours_Users_UserId",
                table: "Hours",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
