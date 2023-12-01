using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SorteioAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemovePessoaIdFromNumero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PessoaId",
                table: "Numero");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PessoaId",
                table: "Numero",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
