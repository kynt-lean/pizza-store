using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain;

namespace PizzaStore.Data;

public class PizzaStoreDbContext(DbContextOptions<PizzaStoreDbContext> options) : IdentityDbContext<PizzaStoreUser>(options)
{
    public DbSet<Order> Orders => Set<Order>();

    public DbSet<Pizza> Pizzas => Set<Pizza>();

    public DbSet<PizzaSpecial> Specials => Set<PizzaSpecial>();

    public DbSet<Topping> Toppings => Set<Topping>();

    public DbSet<NotificationSubscription> NotificationSubscriptions => Set<NotificationSubscription>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuring a many-to-many special -> topping relationship that is friendly for serialization
        modelBuilder.Entity<PizzaTopping>().HasKey(pst => new { pst.PizzaId, pst.ToppingId });
        modelBuilder.Entity<PizzaTopping>().HasOne<Pizza>().WithMany(ps => ps.Toppings);
        modelBuilder.Entity<PizzaTopping>().HasOne(pst => pst.Topping).WithMany();

        // Inline the Lat-Long pairs in Order rather than having a FK to another table
        modelBuilder.Entity<Order>().OwnsOne(o => o.DeliveryLocation);
    }
}
