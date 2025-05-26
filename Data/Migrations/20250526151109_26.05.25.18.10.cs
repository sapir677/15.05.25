using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class _2605251810 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Users");
        }
    }
}
