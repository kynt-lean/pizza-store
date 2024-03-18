namespace PizzaStore.Contracts;

public class OrderDto
{
    public int OrderId { get; set; }

    // Set by the server during POST
    public string? UserId { get; set; }

    public DateTime CreatedTime { get; set; }

    public AddressDto DeliveryAddress { get; set; } = new AddressDto();

    // Set by server during POST
    public LatLongDto? DeliveryLocation { get; set; }

    public List<PizzaDto> Pizzas { get; set; } = [];
}