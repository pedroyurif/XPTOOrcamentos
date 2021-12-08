using Microsoft.EntityFrameworkCore.Migrations;

namespace XPTOOrcamentos.Migrations
{
    public partial class AlteracaoNomeTabelaPrestadorServicos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "PrestadoresServico",
                newName: "CPF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "PrestadoresServico",
                newName: "CNPJ");
        }
    }
}
