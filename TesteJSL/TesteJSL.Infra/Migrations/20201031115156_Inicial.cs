using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TesteJSL.Infra.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Motorista",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCadastro = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "GETDATE()"),
                    Nome = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Cep = table.Column<string>(type: "nvarchar(12)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(2)", nullable: false),
                    EnderecoLatitude = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EnderecoLongitude = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorista", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCadastro = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "GETDATE()"),
                    Login = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCadastro = table.Column<DateTime>(type: "DateTime", nullable: false, defaultValueSql: "GETDATE()"),
                    Marca = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Placa = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Eixos = table.Column<byte>(type: "tinyint", nullable: false),
                    IdMotorista = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculo_Motorista_IdMotorista",
                        column: x => x.IdMotorista,
                        principalTable: "Motorista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_IdMotorista",
                table: "Veiculo",
                column: "IdMotorista");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Veiculo");

            migrationBuilder.DropTable(
                name: "Motorista");
        }
    }
}
