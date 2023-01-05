using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FastMechanical.Migrations
{
    public partial class dataExclusao2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataInsercaoExclusao",
                table: "Estoque",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataInsercaoExclusao",
                table: "Estoque");
        }
    }
}
