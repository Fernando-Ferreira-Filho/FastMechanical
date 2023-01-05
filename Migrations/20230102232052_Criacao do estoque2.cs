using Microsoft.EntityFrameworkCore.Migrations;

namespace FastMechanical.Migrations
{
    public partial class Criacaodoestoque2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoMovimentacao",
                table: "Estoque",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoMovimentacao",
                table: "Estoque");
        }
    }
}
