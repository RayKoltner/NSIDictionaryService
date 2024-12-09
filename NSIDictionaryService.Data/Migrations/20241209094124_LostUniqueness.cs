using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSIDictionaryService.Data.Migrations
{
    /// <inheritdoc />
    public partial class LostUniqueness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Versions_VersionCode",
                table: "Versions");

            migrationBuilder.AlterColumn<string>(
                name: "VersionCode",
                table: "Versions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VersionCode",
                table: "Versions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Versions_VersionCode",
                table: "Versions",
                column: "VersionCode",
                unique: true);
        }
    }
}
