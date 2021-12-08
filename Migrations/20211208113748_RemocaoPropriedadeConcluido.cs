using Microsoft.EntityFrameworkCore.Migrations;

namespace XPTOOrcamentos.Migrations
{
    public partial class RemocaoPropriedadeConcluido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Concluido",
                table: "OrdensServico");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Concluido",
                table: "OrdensServico",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
