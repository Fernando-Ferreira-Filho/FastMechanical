using Microsoft.EntityFrameworkCore.Migrations;

namespace FastMechanical.Migrations
{
    public partial class Unidadedemedida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnidadeMedidade",
                table: "Materiais",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnidadeMedidade",
                table: "Materiais");
        }
    }
}
