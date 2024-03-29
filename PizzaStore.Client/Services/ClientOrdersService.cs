using System.Net.Http.Json;
using PizzaStore.Contracts;

namespace PizzaStore.Client.Services;

public class ClientOrdersService(HttpClient http) : IOrdersService
{
    public async Task<List<OrderWithStatusDto>> GetListOrderWithStatusAsync()
    {
        return await http.GetFromJsonAsync<List<OrderWithStatusDto>>("orders") ?? [];
    }

    public async Task<OrderWithStatusDto> GetOrderWithStatusAsync(int orderId)
    {
        return await http.GetFromJsonAsync<OrderWithStatusDto>($"orders/{orderId}") ?? throw new NullReferenceException();
    }

    public async Task<int> PlaceOrderAsync(OrderDto order)
    {
        return Convert.ToInt32(await (await http.PostAsJsonAsync("orders", order)).Content.ReadAsStringAsync());
    }
}