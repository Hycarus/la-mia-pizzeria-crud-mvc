using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    public partial class AddIngredients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PizzaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ingredients_pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "pizzas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ingredients_PizzaId",
                table: "ingredients",
                column: "PizzaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ingredients");
        }
    }
}
