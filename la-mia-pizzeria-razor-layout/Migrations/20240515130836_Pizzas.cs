using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    public partial class Pizzas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "pizzas",
                columns: new[] { "Id", "Image", "Name", "Overview", "Price" },
                values: new object[,]
                {
                    { 1, "https://www.finedininglovers.it/sites/g/files/xknfdk1106/files/fdl_content_import_it/margherita-50kalo.jpg", "Margherita", "Passata di pomodoro, Mozzarella", 6.50m },
                    { 2, "https://www.petitchef.it/imgupl/recipe/pizza-4-stagioni--449891p695427.jpg", "4 Stagioni", "Passata di pomodoro, Mozzarella, Prosciutto cotto, Carcofini, Funghi, Pomodorini", 8.00m },
                    { 3, "https://www.pizzarecipe.org/wp-content/uploads/2019/01/Pizza-Marinara.jpg", "Marinara", "Passata di pomodoro, Aglio, Olio", 5.50m },
                    { 4, "https://i1.wp.com/www.piccolericette.net/piccolericette/wp-content/uploads/2019/10/4102_Pizza.jpg?resize=895%2C616&ssl=1", "Prosciutto e Funghi", "Passata di pomodoro, Mozzarella, Prosciutto cotto, Funghi", 7.50m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "pizzas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "pizzas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "pizzas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "pizzas",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
