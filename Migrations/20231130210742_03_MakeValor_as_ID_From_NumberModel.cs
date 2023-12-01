using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SorteioAPI.Migrations
{
    /// <inheritdoc />
    public partial class _03_MakeValor_as_ID_From_NumberModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Numero",
                table: "Numero");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Numero");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Numero",
                table: "Numero",
                column: "Valor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Numero",
                table: "Numero");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Numero",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Numero",
                table: "Numero",
                column: "Id");
        }
    }
}
