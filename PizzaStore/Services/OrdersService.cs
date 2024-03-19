using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Contracts;
using PizzaStore.Data;
using PizzaStore.Domain;

namespace PizzaStore.Services;

public class OrdersService(PizzaStoreDbContext db, IMapper mapper) : IOrdersService
{
    private readonly PizzaStoreDbContext _db = db;
    private readonly IMapper _mapper = mapper;

    public async Task<List<OrderWithStatusDto>> GetListOrderWithStatusAsync()
    {
        var orders = await _db.Orders
            // .Where(o => o.UserId == PizzaStoreApiExtensions.GetUserId(HttpContext))
            .Include(o => o.DeliveryLocation)
            .Include(o => o.Pizzas).ThenInclude(p => p.Special)
            .Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
            .OrderByDescending(o => o.CreatedTime)
            .ToListAsync();

        return orders.Select(o => OrderWithStatusDto.FromOrder(_mapper.Map<OrderDto>(o))).ToList();
    }

    public async Task<OrderWithStatusDto> GetOrderWithStatusAsync(int orderId)
    {
        var order = await _db.Orders
            .Where(o => o.OrderId == orderId)
            // .Where(o => o.UserId == PizzaStoreApiExtensions.GetUserId(HttpContext))
            .Include(o => o.DeliveryLocation)
            .Include(o => o.Pizzas).ThenInclude(p => p.Special)
            .Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping)
            .SingleOrDefaultAsync();

        return order == null
            ? throw new NullReferenceException()
            : OrderWithStatusDto.FromOrder(_mapper.Map<OrderDto>(order));
    }

    public async Task<int> PlaceOrderAsync(OrderDto order)
    {
        var creatingOrder = _mapper.Map<Order>(order);
        creatingOrder.CreatedTime = DateTime.Now;
        creatingOrder.DeliveryLocation = new LatLong(51.5001, -0.1239);
        // order.UserId = PizzaStoreApiExtensions.GetUserId(HttpContext);

        // Enforce existence of PizzaStore.SpecialId and Topping.ToppingId
        // in the database - prevent the submitter from making up
        // new specials and toppings
        foreach (var pizza in creatingOrder.Pizzas)
        {
            pizza.SpecialId = pizza.Special?.Id ?? 0;
            pizza.Special = null;

            foreach (var topping in pizza.Toppings)
            {
                topping.ToppingId = topping.Topping?.Id ?? 0;
                topping.Topping = null;
            }
        }

        _db.Orders.Attach(creatingOrder);
        await _db.SaveChangesAsync();

        // In the background, send push notifications if possible
        // var subscription = await _db.NotificationSubscriptions.Where(e => e.UserId == PizzaApiExtensions.GetUserId(HttpContext)).SingleOrDefaultAsync();
        // if (subscription != null)
        // {
        //     _ = TrackAndSendNotificationsAsync(order, subscription);
        // }

        return creatingOrder.OrderId;
    }

    public Task<decimal> GetOrderTotalPriceAsync(OrderDto order)
    {
        return Task.FromResult(_mapper.Map<Order>(order).GetTotalPrice());
    }

    private static async Task TrackAndSendNotificationsAsync(Order order, NotificationSubscription subscription)
    {
        // In a realistic case, some other backend process would track
        // order delivery progress and send us notifications when it
        // changes. Since we don't have any such process here, fake it.
        await Task.Delay(OrderWithStatusDto.PreparationDuration);
        await SendNotificationAsync(order, subscription, "Your order has been dispatched!");

        await Task.Delay(OrderWithStatusDto.DeliveryDuration);
        await SendNotificationAsync(order, subscription, "Your order is now delivered. Enjoy!");
    }

    private static Task SendNotificationAsync(Order order, NotificationSubscription subscription, string message)
    {
        // This will be implemented later
        return Task.CompletedTask;
    }
}