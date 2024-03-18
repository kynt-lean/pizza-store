using System.Net.Http.Json;
using PizzaStore.Contracts;

namespace PizzaStore.Client.Services;

public class ClientOrdersService(HttpClient http) : IOrdersService
{
    public async Task<List<OrderWithStatusDto>> GetListOrderWithStatusAsync()
    {
        return await http.GetFromJsonAsync<List<OrderWithStatusDto>>("orders") ?? [];
    }

    public async Task<decimal> GetOrderTotalPriceAsync(OrderDto order)
    {
        return Convert.ToDecimal(await (await http.PostAsJsonAsync("orders/total-price", order)).Content.ReadAsStringAsync());
    }

    public async Task<OrderWithStatusDto> GetOrderWithStatusAsync(int orderId)
    {
        return await http.GetFromJsonAsync<OrderWithStatusDto>($"orders/{orderId}") ?? throw new Exception();
    }

    public async Task<int> PlaceOrderAsync(OrderDto order)
    {
        return Convert.ToInt32(await (await http.PostAsJsonAsync("orders", order)).Content.ReadAsStringAsync());
    }
}