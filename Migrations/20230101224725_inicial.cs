using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FastMechanical.Migrations {
    public partial class inicial : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "Materiais",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UnidadeMedidade = table.Column<int>(nullable: false),
                    ValorCusto = table.Column<double>(nullable: false),
                    PorcentagemLucro = table.Column<double>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Materiais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Telefone = table.Column<long>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Cpf = table.Column<string>(maxLength: 11, nullable: false),
                    Rua = table.Column<string>(maxLength: 20, nullable: false),
                    Bairro = table.Column<string>(maxLength: 40, nullable: false),
                    Estado = table.Column<string>(maxLength: 20, nullable: false),
                    Complemento = table.Column<string>(maxLength: 40, nullable: true),
                    Cidade = table.Column<string>(maxLength: 20, nullable: false),
                    Numero = table.Column<string>(maxLength: 7, nullable: true),
                    DataDeNascimento = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    TipoPessoa = table.Column<int>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Valor = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Servicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Renavam = table.Column<string>(maxLength: 11, nullable: false),
                    Placa = table.Column<string>(maxLength: 7, nullable: false),
                    Modelo = table.Column<string>(maxLength: 255, nullable: false),
                    Marca = table.Column<string>(maxLength: 30, nullable: false),
                    AnoDeFabricacao = table.Column<int>(nullable: false),
                    Cor = table.Column<string>(maxLength: 25, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    PessoaId = table.Column<int>(nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculo_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_Cpf",
                table: "Pessoa",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_PessoaId",
                table: "Veiculo",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_Placa",
                table: "Veiculo",
                column: "Placa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_Renavam",
                table: "Veiculo",
                column: "Renavam",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "Materiais");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropTable(
                name: "Veiculo");

            migrationBuilder.DropTable(
                name: "Pessoa");
        }
    }
}
