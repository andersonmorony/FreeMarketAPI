using Microsoft.EntityFrameworkCore.Migrations;

namespace Unico.Core.API.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Markets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LONG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LAT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SETCENS = table.Column<long>(type: "bigint", nullable: false),
                    AREAP = table.Column<long>(type: "bigint", nullable: false),
                    CODDIST = table.Column<int>(type: "int", nullable: false),
                    DISTRITO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CODSUBPREF = table.Column<int>(type: "int", nullable: false),
                    SUBPREFE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    REGIAO5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    REGIAO8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NOME_FEIRA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    REGISTRO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LOGRADOURO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NUMERO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BAIRRO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    REFERENCIA = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Markets");
        }
    }
}
