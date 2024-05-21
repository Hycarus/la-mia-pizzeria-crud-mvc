using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    public partial class AddIngredients2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ingredients",
                columns: new[] { "Id", "Name", "PizzaId" },
                values: new object[] { 1, "Mozzarella", null });

            migrationBuilder.InsertData(
                table: "ingredients",
                columns: new[] { "Id", "Name", "PizzaId" },
                values: new object[] { 2, "Pomodoro", null });

            migrationBuilder.InsertData(
                table: "ingredients",
                columns: new[] { "Id", "Name", "PizzaId" },
                values: new object[] { 3, "Basilico", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ingredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ingredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ingredients",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
