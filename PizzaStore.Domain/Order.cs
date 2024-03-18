namespace PizzaStore.Domain;

public class Order
{
    public int OrderId { get; set; }

    public string? UserId { get; set; }

    public DateTime CreatedTime { get; set; }

    public Address DeliveryAddress { get; set; } = new Address();

    public LatLong? DeliveryLocation { get; set; }

    public List<Pizza> Pizzas { get; set; } = [];

    public decimal GetTotalPrice() => Pizzas.Sum(p => p.GetTotalPrice());
}