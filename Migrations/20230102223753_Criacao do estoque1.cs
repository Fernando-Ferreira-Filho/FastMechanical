using Microsoft.EntityFrameworkCore.Migrations;

namespace FastMechanical.Migrations
{
    public partial class Criacaodoestoque1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChaveAcessoNotaFiscal",
                table: "Estoque",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroNotaFiscal",
                table: "Estoque",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChaveAcessoNotaFiscal",
                table: "Estoque");

            migrationBuilder.DropColumn(
                name: "NumeroNotaFiscal",
                table: "Estoque");
        }
    }
}
