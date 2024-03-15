namespace PizzaStore.Contracts;

public class PizzaToppingDto
{
    public ToppingDto? Topping { get; set; }

    public int ToppingId { get; set; }

    public int PizzaId { get; set; }
}