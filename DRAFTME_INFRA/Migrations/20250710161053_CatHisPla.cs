using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DRAFTME_INFRA.Migrations
{
    /// <inheritdoc />
    public partial class CatHisPla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricosPlayer_HistoricosTeam_HistoricoTeamId",
                table: "HistoricosPlayer");

            migrationBuilder.AlterColumn<int>(
                name: "HistoricoTeamId",
                table: "HistoricosPlayer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricosPlayer_HistoricosTeam_HistoricoTeamId",
                table: "HistoricosPlayer",
                column: "HistoricoTeamId",
                principalTable: "HistoricosTeam",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoricosPlayer_HistoricosTeam_HistoricoTeamId",
                table: "HistoricosPlayer");

            migrationBuilder.AlterColumn<int>(
                name: "HistoricoTeamId",
                table: "HistoricosPlayer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricosPlayer_HistoricosTeam_HistoricoTeamId",
                table: "HistoricosPlayer",
                column: "HistoricoTeamId",
                principalTable: "HistoricosTeam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
