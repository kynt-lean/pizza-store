namespace PizzaStore.Domain;

public class Order
{
    public int OrderId { get; set; }

    // Set by the server during POST
    public string? UserId { get; set; }

    public DateTime CreatedTime { get; set; }

    public Address DeliveryAddress { get; set; } = new Address();

    // Set by server during POST
    public LatLong? DeliveryLocation { get; set; }

    public List<Pizza> Pizzas { get; set; } = [];

    public decimal GetTotalPrice() => Pizzas.Sum(p => p.GetTotalPrice());

    public string GetFormattedTotalPrice() => GetTotalPrice().ToString("0.00");
}