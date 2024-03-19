using Microsoft.AspNetCore.Mvc;
using PizzaStore.Contracts;

namespace PizzaStore.Controllers;

[Route("orders")]
[ApiController]
public class OrdersController(IOrdersService service) : Controller
{
    private readonly IOrdersService _service = service;

    [HttpGet]
    public async Task<ActionResult<List<OrderWithStatusDto>>> GetListOrderWithStatusAsync()
    {
        return await _service.GetListOrderWithStatusAsync();
    }

    [HttpGet("{orderId}")]
    public async Task<ActionResult<OrderWithStatusDto>> GetOrderWithStatusAsync(int orderId)
    {
        try
        {
            return await _service.GetOrderWithStatusAsync(orderId);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<int>> PlaceOrderAsync(OrderDto order)
    {
        return await _service.PlaceOrderAsync(order);
    }
}