using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    public partial class EditTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ingredients_pizzas_PizzaId",
                table: "ingredients");

            migrationBuilder.DropIndex(
                name: "IX_ingredients_PizzaId",
                table: "ingredients");

            migrationBuilder.DropColumn(
                name: "PizzaId",
                table: "ingredients");

            migrationBuilder.CreateTable(
                name: "IngredientPizza",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "int", nullable: false),
                    PizzasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientPizza", x => new { x.IngredientsId, x.PizzasId });
                    table.ForeignKey(
                        name: "FK_IngredientPizza_ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientPizza_pizzas_PizzasId",
                        column: x => x.PizzasId,
                        principalTable: "pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientPizza_PizzasId",
                table: "IngredientPizza",
                column: "PizzasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientPizza");

            migrationBuilder.AddColumn<int>(
                name: "PizzaId",
                table: "ingredients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ingredients_PizzaId",
                table: "ingredients",
                column: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ingredients_pizzas_PizzaId",
                table: "ingredients",
                column: "PizzaId",
                principalTable: "pizzas",
                principalColumn: "Id");
        }
    }
}
