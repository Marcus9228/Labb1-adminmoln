using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb2_webbutv.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item__Pokemons_Pokemonid",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "_Items");

            migrationBuilder.RenameIndex(
                name: "IX_Item_Pokemonid",
                table: "_Items",
                newName: "IX__Items_Pokemonid");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Items",
                table: "_Items",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__Items__Pokemons_Pokemonid",
                table: "_Items",
                column: "Pokemonid",
                principalTable: "_Pokemons",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Items__Pokemons_Pokemonid",
                table: "_Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Items",
                table: "_Items");

            migrationBuilder.RenameTable(
                name: "_Items",
                newName: "Item");

            migrationBuilder.RenameIndex(
                name: "IX__Items_Pokemonid",
                table: "Item",
                newName: "IX_Item_Pokemonid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item__Pokemons_Pokemonid",
                table: "Item",
                column: "Pokemonid",
                principalTable: "_Pokemons",
                principalColumn: "id");
        }
    }
}
