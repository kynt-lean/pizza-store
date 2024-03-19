using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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

        return app;
    }

    public static string? GetUserId(HttpContext context) => context.User.FindFirstValue(ClaimTypes.NameIdentifier);
}