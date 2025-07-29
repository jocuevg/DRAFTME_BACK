using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DRAFTME_INFRA.Migrations
{
    /// <inheritdoc />
    public partial class Ofertas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Scouters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ofertas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    Posicion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ofertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ofertas_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Scouters_TeamId",
                table: "Scouters",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_TeamId",
                table: "Ofertas",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scouters_Teams_TeamId",
                table: "Scouters",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scouters_Teams_TeamId",
                table: "Scouters");

            migrationBuilder.DropTable(
                name: "Ofertas");

            migrationBuilder.DropIndex(
                name: "IX_Scouters_TeamId",
                table: "Scouters");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Scouters");
        }
    }
}
