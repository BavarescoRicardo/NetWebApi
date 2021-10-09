using Microsoft.EntityFrameworkCore.Migrations;

namespace NetWebApi.Migrations
{
    public partial class Inserir_dados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Nome", "Opcao", "Quantidade", "Valor" },
                values: new object[] { 1, "Net visualsutido", 1, 1.0, 2.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
