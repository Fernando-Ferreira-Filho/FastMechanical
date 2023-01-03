using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FastMechanical.Migrations
{
    public partial class Criacaodoestoque6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAdicao",
                table: "Estoque",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataBaixa",
                table: "Estoque",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAdicao",
                table: "Estoque");

            migrationBuilder.DropColumn(
                name: "DataBaixa",
                table: "Estoque");
        }
    }
}
