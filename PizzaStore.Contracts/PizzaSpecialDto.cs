namespace PizzaStore.Contracts;

public class PizzaSpecialDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal BasePrice { get; set; }

    public string Description { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }
}