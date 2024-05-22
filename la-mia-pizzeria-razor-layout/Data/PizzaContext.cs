using System;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Data
{
    public class PizzaContext : IdentityDbContext<IdentityUser>
    {
        //public PizzaContext(DbContextOptions<PizzaContext> options) : base(options) { }
        public DbSet<Pizza>? Pizzas { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Ingredient>? Ingredients { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost,1433;Database=pizzeria_db;User Id=sa;Password=dockerStrongPwd123;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>().Property(m => m.CategoryId).IsRequired(false);

            modelBuilder.Entity<Pizza>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Pizzas)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Pizze Bianche" },
                new Category { Id = 2, Name = "Pizze Vegetariane" },
                new Category { Id = 3, Name = "Pizze di Mare" },
                new Category { Id = 4, Name = "Pizze Speciali" },
                new Category { Id = 5, Name = "Calzoni" },
                new Category { Id = 6, Name = "Pizze Fritte" },
                new Category { Id = 7, Name = "Pizze al Padellino" }
                );

            modelBuilder.Entity<Pizza>().HasData(
                new Pizza { Id = 1, Name = "Margherita", Overview = "Passata di pomodoro, Mozzarella", Image = "https://www.finedininglovers.it/sites/g/files/xknfdk1106/files/fdl_content_import_it/margherita-50kalo.jpg", Price = 6.50m, CategoryId = 1 },
                new Pizza { Id = 2, Name = "4 Stagioni", Overview = "Passata di pomodoro, Mozzarella, Prosciutto cotto, Carcofini, Funghi, Pomodorini", Image = "https://www.petitchef.it/imgupl/recipe/pizza-4-stagioni--449891p695427.jpg", Price = 8.00m, CategoryId = 1 },
                new Pizza { Id = 3, Name = "Marinara", Overview = "Passata di pomodoro, Aglio, Olio", Image = "https://www.pizzarecipe.org/wp-content/uploads/2019/01/Pizza-Marinara.jpg", Price = 5.50m, CategoryId = 1 },
                new Pizza { Id = 4, Name = "Prosciutto e Funghi", Overview = "Passata di pomodoro, Mozzarella, Prosciutto cotto, Funghi", Image = "https://i1.wp.com/www.piccolericette.net/piccolericette/wp-content/uploads/2019/10/4102_Pizza.jpg?resize=895%2C616&ssl=1", Price = 7.50m, CategoryId = 1 }
                );

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "Mozzarella" },
                new Ingredient { Id = 2, Name = "Pomodoro" },
                new Ingredient { Id = 3, Name = "Basilico" }
                );




            base.OnModelCreating(modelBuilder);

        }
    }
}
