using Microsoft.EntityFrameworkCore.Migrations;

namespace FastMechanical.Migrations
{
    public partial class cores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Vendedor",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Modelo",
                table: "Veiculo",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(7) CHARACTER SET utf8mb4",
                oldMaxLength: 7);

            migrationBuilder.AlterColumn<string>(
                name: "Marca",
                table: "Veiculo",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(7) CHARACTER SET utf8mb4",
                oldMaxLength: 7);

            migrationBuilder.AlterColumn<string>(
                name: "Cor",
                table: "Veiculo",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(7) CHARACTER SET utf8mb4",
                oldMaxLength: 7);

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Mecanico",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Cliente",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Vendedor",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "Modelo",
                table: "Veiculo",
                type: "varchar(7) CHARACTER SET utf8mb4",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Marca",
                table: "Veiculo",
                type: "varchar(7) CHARACTER SET utf8mb4",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Cor",
                table: "Veiculo",
                type: "varchar(7) CHARACTER SET utf8mb4",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Mecanico",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Cliente",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 11);
        }
    }
}
