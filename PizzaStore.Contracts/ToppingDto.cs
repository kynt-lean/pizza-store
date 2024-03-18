namespace PizzaStore.Contracts;

public class ToppingDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }
}