using Microsoft.EntityFrameworkCore.Migrations;

namespace MangaExpressController.Data.Migrations
{
    public partial class TablaMangaUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MangaUsuarios",
                columns: table => new
                {
                    UID = table.Column<string>(nullable: false),
                    MID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaUsuarios", x => new { x.UID, x.MID });
                    table.ForeignKey(
                        name: "FK_MangaUsuarios_Mangas_MID",
                        column: x => x.MID,
                        principalTable: "Mangas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MangaUsuarios_Usuarios_UID",
                        column: x => x.UID,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MangaUsuarios_MID",
                table: "MangaUsuarios",
                column: "MID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MangaUsuarios");
        }
    }
}
