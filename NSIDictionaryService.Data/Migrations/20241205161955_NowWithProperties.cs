using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSIDictionaryService.Data.Migrations
{
    /// <inheritdoc />
    public partial class NowWithProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "Versions",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "V025",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "V021",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "V012",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "V006",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "Users",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "Uploads",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "Roles",
                newName: "DeletedDate");

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DictionaryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "date", nullable: false),
                    EditUserId = table.Column<int>(type: "int", nullable: false),
                    EditDate = table.Column<DateTime>(type: "date", nullable: false),
                    DeletedUserId = table.Column<int>(type: "int", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "Versions",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "V025",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "V021",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "V012",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "V006",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "Users",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "Uploads",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "Roles",
                newName: "DeleteDate");
        }
    }
}
