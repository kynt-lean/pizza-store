using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Contracts;
using PizzaStore.Data;
using PizzaStore.Domain;

namespace Microsoft.AspNetCore.Routing;

public static class PizzaApiExtensions
{
    public static WebApplication MapPizzaApiEndpoints(this WebApplication app)
    {
        // Subscribe to notifications
        app.MapPut("/notifications/subscribe", [Authorize] async (
            HttpContext context,
            PizzaStoreDbContext db,
            NotificationSubscription subscription) =>
        {
            // We're storing at most one subscription per user, so delete old ones.
            // Alternatively, you could let the user register multiple subscriptions from different browsers/devices.
            var userId = GetUserId(context);
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            var oldSubscriptions = db.NotificationSubscriptions.Where(e => e.UserId == userId);
            db.NotificationSubscriptions.RemoveRange(oldSubscriptions);

            // Store new subscription
            subscription.UserId = userId;
            db.NotificationSubscriptions.Attach(subscription);

            await db.SaveChangesAsync();
            return Results.Ok(subscription);
        });

        // Specials
        app.MapGet("pizza/specials", async (IPizzaService service) =>
        {

            var specials = await service.GetListSpecialAsync();
            return Results.Ok(specials);

        });

        // Toppings
        app.MapGet("pizza/toppings", async (IPizzaService service) =>
        {
            var toppings = await service.GetListToppingAsync();
            return Results.Ok(toppings);
        });

        // Pizza base price
        app.MapPost("pizza/base-price", async ([FromBody] PizzaDto pizza, IPizzaService service) =>
        {
            var basePrice = await service.GetPizzaBasePriceAsync(pizza);
            return Results.Ok(basePrice);
        });

        // Pizza total price
        app.MapPost("pizza/total-price", async ([FromBody] PizzaDto pizza, IPizzaService service) =>
        {
            var totalPrice = await service.GetPizzaTotalPriceAsync(pizza);
            return Results.Ok(totalPrice);
        });

        return app;
    }

    public static string? GetUserId(HttpContext context) => context.User.FindFirstValue(ClaimTypes.NameIdentifier);
}