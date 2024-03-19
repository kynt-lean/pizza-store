namespace PizzaStore.Contracts;

public interface IOrdersService
{
    Task<List<OrderWithStatusDto>> GetListOrderWithStatusAsync();

    Task<OrderWithStatusDto> GetOrderWithStatusAsync(int orderId);

    Task<int> PlaceOrderAsync(OrderDto order);
}