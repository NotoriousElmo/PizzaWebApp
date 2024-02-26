using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppDbContext : DbContext
{
    public DbSet<AdditionalComponent> AdditionalComponents { get; set; } = default!;
    public DbSet<AdditionalComponentInPizza> AdditionalComponentsInPizzas { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<Pizza> Pizzas { get; set; } = default!;
    public DbSet<PizzaInOrder> PizzaInOrders { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

}