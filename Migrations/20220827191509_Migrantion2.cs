using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FastMechanical.Migrations
{
    public partial class Migrantion2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_Pessoa_PessoaId",
                table: "Veiculo");

            migrationBuilder.DropTable(
                name: "Pessoa");

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Telefone = table.Column<long>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Cpf = table.Column<string>(nullable: false),
                    Rua = table.Column<string>(maxLength: 20, nullable: false),
                    Bairro = table.Column<string>(maxLength: 40, nullable: false),
                    Estado = table.Column<string>(maxLength: 20, nullable: false),
                    Complemento = table.Column<string>(maxLength: 40, nullable: true),
                    Cidade = table.Column<string>(maxLength: 20, nullable: false),
                    Numero = table.Column<string>(maxLength: 7, nullable: true),
                    DataDeNascimento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Cpf",
                table: "Cliente",
                column: "Cpf",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_Cliente_PessoaId",
                table: "Veiculo",
                column: "PessoaId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_Cliente_PessoaId",
                table: "Veiculo");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Bairro = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Cidade = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Complemento = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Cpf = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    DataDeNascimento = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Email = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Estado = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Nome = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Numero = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Rua = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Telefone = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_Cpf",
                table: "Pessoa",
                column: "Cpf",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_Pessoa_PessoaId",
                table: "Veiculo",
                column: "PessoaId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
