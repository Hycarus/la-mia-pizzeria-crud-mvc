using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pizzas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Overview = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pizzas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pizzas_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pizze Bianche" },
                    { 2, "Pizze Vegetariane" },
                    { 3, "Pizze di Mare" },
                    { 4, "Pizze Speciali" },
                    { 5, "Calzoni" },
                    { 6, "Pizze Fritte" },
                    { 7, "Pizze al Padellino" }
                });

            migrationBuilder.InsertData(
                table: "pizzas",
                columns: new[] { "Id", "CategoryId", "Image", "Name", "Overview", "Price" },
                values: new object[,]
                {
                    { 1, 1, "https://www.finedininglovers.it/sites/g/files/xknfdk1106/files/fdl_content_import_it/margherita-50kalo.jpg", "Margherita", "Passata di pomodoro, Mozzarella", 6.50m },
                    { 2, 1, "https://www.petitchef.it/imgupl/recipe/pizza-4-stagioni--449891p695427.jpg", "4 Stagioni", "Passata di pomodoro, Mozzarella, Prosciutto cotto, Carcofini, Funghi, Pomodorini", 8.00m },
                    { 3, 1, "https://www.pizzarecipe.org/wp-content/uploads/2019/01/Pizza-Marinara.jpg", "Marinara", "Passata di pomodoro, Aglio, Olio", 5.50m },
                    { 4, 1, "https://i1.wp.com/www.piccolericette.net/piccolericette/wp-content/uploads/2019/10/4102_Pizza.jpg?resize=895%2C616&ssl=1", "Prosciutto e Funghi", "Passata di pomodoro, Mozzarella, Prosciutto cotto, Funghi", 7.50m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_pizzas_CategoryId",
                table: "pizzas",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pizzas");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
