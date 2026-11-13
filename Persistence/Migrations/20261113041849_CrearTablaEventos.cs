using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSW_3P_PR01.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaEventos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Eventos",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Lugar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CupoMaximo = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.CheckConstraint("CK_Eventos_CupoMaximo", "[CupoMaximo] > 0");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eventos",
                schema: "dbo");
        }
    }
}
