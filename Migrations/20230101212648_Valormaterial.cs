using Microsoft.EntityFrameworkCore.Migrations;

namespace FastMechanical.Migrations
{
    public partial class Valormaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "PorcentagemLucro",
                table: "Materiais",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ValorCusto",
                table: "Materiais",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PorcentagemLucro",
                table: "Materiais");

            migrationBuilder.DropColumn(
                name: "ValorCusto",
                table: "Materiais");
        }
    }
}
