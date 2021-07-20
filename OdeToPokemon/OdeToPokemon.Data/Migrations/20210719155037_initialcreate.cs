using Microsoft.EntityFrameworkCore.Migrations;

namespace OdeToPokemon.Data.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    NextEvolutionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Pokemons_Pokemons_NextEvolutionName",
                        column: x => x.NextEvolutionName,
                        principalTable: "Pokemons",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_NextEvolutionName",
                table: "Pokemons",
                column: "NextEvolutionName",
                unique: true,
                filter: "[NextEvolutionName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pokemons");
        }
    }
}
