using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PizzaStore.Domain;

namespace PizzaStore;

public class PizzaStoreContext : ApiAuthorizationDbContext<PizzaStoreUser>
{
    public PizzaStoreContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions)
    : base(options, operationalStoreOptions)
    {
    }

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<Pizza> Pizzas => Set<Pizza>();

    public DbSet<PizzaSpecial> Specials => Set<PizzaSpecial>();

    public DbSet<Topping> Toppings => Set<Topping>();

    public DbSet<NotificationSubscription> NotificationSubscriptions => Set<NotificationSubscription>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuring a many-to-many special -> topping relationship that is friendly for serialization
        modelBuilder.Entity<PizzaTopping>().HasKey(pst => new { pst.PizzaStoreId, pst.ToppingId });
        modelBuilder.Entity<PizzaTopping>().HasOne<Pizza>().WithMany(ps => ps.Toppings);
        modelBuilder.Entity<PizzaTopping>().HasOne(pst => pst.Topping).WithMany();

        // Inline the Lat-Long pairs in Order rather than having a FK to another table
        modelBuilder.Entity<Order>().OwnsOne(o => o.DeliveryLocation);
    }
}