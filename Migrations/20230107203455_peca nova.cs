using Microsoft.EntityFrameworkCore.Migrations;

namespace FastMechanical.Migrations
{
    public partial class pecanova : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtendimentoId",
                table: "Estoque");

            migrationBuilder.AddColumn<int>(
                name: "AgendaId",
                table: "Estoque",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estoque_AgendaId",
                table: "Estoque",
                column: "AgendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoque_Agenda_AgendaId",
                table: "Estoque",
                column: "AgendaId",
                principalTable: "Agenda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoque_Agenda_AgendaId",
                table: "Estoque");

            migrationBuilder.DropIndex(
                name: "IX_Estoque_AgendaId",
                table: "Estoque");

            migrationBuilder.DropColumn(
                name: "AgendaId",
                table: "Estoque");

            migrationBuilder.AddColumn<int>(
                name: "AtendimentoId",
                table: "Estoque",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
