using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain;

namespace PizzaStore.Data;

public static class SeedData
{
    public static async Task InitializeAsync(PizzaStoreDbContext db)
    {
        var toppings = new Topping[]
        {
            new()
            {
                Name = "Extra cheese",
                Price = 2.50m,
            },
            new()
            {
                Name = "American bacon",
                Price = 2.99m,
            },
            new()
            {
                Name = "British bacon",
                Price = 2.99m,
            },
            new()
            {
                Name = "Canadian bacon",
                Price = 2.99m,
            },
            new()
            {
                Name = "Tea and crumpets",
                Price = 5.00m
            },
            new()
            {
                Name = "Fresh-baked scones",
                Price = 4.50m,
            },
            new()
            {
                Name = "Bell peppers",
                Price = 1.00m,
            },
            new()
            {
                Name = "Onions",
                Price = 1.00m,
            },
            new()
            {
                Name = "Mushrooms",
                Price = 1.00m,
            },
            new()
            {
                Name = "Pepperoni",
                Price = 1.00m,
            },
            new()
            {
                Name = "Duck sausage",
                Price = 3.20m,
            },
            new()
            {
                Name = "Venison meatballs",
                Price = 2.50m,
            },
            new()
            {
                Name = "Served on a silver platter",
                Price = 250.99m,
            },
            new()
            {
                Name = "Lobster on top",
                Price = 64.50m,
            },
            new()
            {
                Name = "Sturgeon caviar",
                Price = 101.75m,
            },
            new()
            {
                Name = "Artichoke hearts",
                Price = 3.40m,
            },
            new()
            {
                Name = "Fresh tomatoes",
                Price = 1.50m,
            },
            new()
            {
                Name = "Basil",
                Price = 1.50m,
            },
            new()
            {
                Name = "Steak (medium-rare)",
                Price = 8.50m,
            },
            new()
            {
                Name = "Blazing hot peppers",
                Price = 4.20m,
            },
            new()
            {
                Name = "Buffalo chicken",
                Price = 5.00m,
            },
            new()
            {
                Name = "Blue cheese",
                Price = 2.50m,
            },
        };

        var specials = new PizzaSpecial[]
        {
            new()
            {
                Id = 1,
                Name = "Basic Cheese PizzaStore",
                Description = "It's cheesy and delicious. Why wouldn't you want one?",
                BasePrice = 9.99m,
                ImageUrl = "img/pizzas/cheese.jpg",
            },
            new()
            {
                Id = 2,
                Name = "The Baconatorizor",
                Description = "It has EVERY kind of bacon",
                BasePrice = 11.99m,
                ImageUrl = "img/pizzas/bacon.jpg",
            },
            new()
            {
                Id = 3,
                Name = "Classic pepperoni",
                Description = "It's the pizza you grew up with, but Blazing hot!",
                BasePrice = 10.50m,
                ImageUrl = "img/pizzas/pepperoni.jpg",
            },
            new()
            {
                Id = 4,
                Name = "Buffalo chicken",
                Description = "Spicy chicken, hot sauce and bleu cheese, guaranteed to warm you up",
                BasePrice = 12.75m,
                ImageUrl = "img/pizzas/meaty.jpg",
            },
            new()
            {
                Id = 5,
                Name = "Mushroom Lovers",
                Description = "It has mushrooms. Isn't that obvious?",
                BasePrice = 11.00m,
                ImageUrl = "img/pizzas/mushroom.jpg",
            },
            new()
            {
                Id = 6,
                Name = "The Brit",
                Description = "When in London...",
                BasePrice = 10.25m,
                ImageUrl = "img/pizzas/brit.jpg",
            },
            new()
            {
                Id = 7,
                Name = "Veggie Delight",
                Description = "It's like salad, but on a pizza",
                BasePrice = 11.50m,
                ImageUrl = "img/pizzas/salad.jpg",
            },
            new()
            {
                Id = 8,
                Name = "Margherita",
                Description = "Traditional Italian pizza with tomatoes and basil",
                BasePrice = 9.99m,
                ImageUrl = "img/pizzas/margherita.jpg",
            },
        };

        var existingToppings = await db.Toppings.ToListAsync();
        var existingSpecials = await db.Specials.ToListAsync();

        var updatingToppings = existingToppings.IntersectBy(toppings.Select(e => e.Name), t => t.Name).ToArray();
        var updatingSpecials = existingSpecials.IntersectBy(specials.Select(e => e.Id), s => s.Id).ToArray();

        if (updatingToppings.Length > 0) db.Toppings.UpdateRange(updatingToppings.Select(topping =>
        {
            topping.Price = toppings.FirstOrDefault(t => t.Name == topping.Name)?.Price ?? topping.Price;
            return topping;
        }));
        if (updatingSpecials.Length > 0) db.Specials.UpdateRange(updatingSpecials.Select(special =>
        {
            var updatingSpecial = specials.FirstOrDefault(s => s.Id == special.Id);
            if (updatingSpecial != null)
            {
                special.Name = updatingSpecial.Name;
                special.Description = updatingSpecial.Description;
                special.BasePrice = updatingSpecial.BasePrice;
                special.ImageUrl = updatingSpecial.ImageUrl;
            }
            return special;
        }));

        var newToppings = toppings.ExceptBy(existingToppings.Select(e => e.Name), t => t.Name).ToArray();
        var newSpecials = specials.ExceptBy(existingSpecials.Select(e => e.Name), t => t.Name).ToArray();

        if (newToppings.Length > 0) db.Toppings.AddRange(newToppings);
        if (newSpecials.Length > 0) db.Specials.AddRange(newSpecials);

        await db.SaveChangesAsync();
    }
}