using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppASPNET.Migrations
{
    /// <inheritdoc />
    public partial class addtablefriendshi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Friendship",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Friendship",
                newName: "id");
        }
    }
}
