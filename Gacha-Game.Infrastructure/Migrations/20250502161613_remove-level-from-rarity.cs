using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gacha_Game.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removelevelfromrarity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Rarities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Rarities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
