using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shortener.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pastes",
                columns: table => new
                {
                    PasteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Shortlink = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expires = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pastes", x => x.PasteId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pastes_Shortlink",
                table: "Pastes",
                column: "Shortlink",
                unique: true,
                filter: "[Shortlink] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pastes");
        }
    }
}
