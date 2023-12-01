using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SorteioAPI.Migrations
{
    /// <inheritdoc />
    public partial class _01_PrimeiraMigracao_Pessoas_Numeros_Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Email = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Nome = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Cpf = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    Telefone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Numero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Valor = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    PessoaId = table.Column<int>(type: "integer", nullable: false),
                    PessoaEmail = table.Column<string>(type: "character varying(40)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Numero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Numero_Pessoa_PessoaEmail",
                        column: x => x.PessoaEmail,
                        principalTable: "Pessoa",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Numero_PessoaEmail",
                table: "Numero",
                column: "PessoaEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Numero");

            migrationBuilder.DropTable(
                name: "Pessoa");
        }
    }
}
