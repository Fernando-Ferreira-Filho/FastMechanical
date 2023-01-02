using Microsoft.EntityFrameworkCore.Migrations;

namespace FastMechanical.Migrations {
    public partial class Logincriado : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Pessoa",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Pessoa");
        }
    }
}
