using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetoTecnico.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evaluaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PesoKilogramos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AlturaCentimetros = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorImc = table.Column<decimal>(type: "decimal(18,1)", nullable: false),
                    DescripcionResultado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEvaluacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RangosImc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMinimo = table.Column<decimal>(type: "decimal(18,1)", nullable: true),
                    ValorMaximo = table.Column<decimal>(type: "decimal(18,1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangosImc", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evaluaciones");

            migrationBuilder.DropTable(
                name: "RangosImc");
        }
    }
}
