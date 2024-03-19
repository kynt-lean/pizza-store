namespace PizzaStore.Contracts;

public class PizzaDto
{
    public const int DefaultSize = 12;
    public const int MinimumSize = 9;
    public const int MaximumSize = 17;

    public int Id { get; set; }

    public int OrderId { get; set; }

    public PizzaSpecialDto? Special { get; set; }

    public int SpecialId { get; set; }

    public int Size { get; set; }

    public List<PizzaToppingDto> Toppings { get; set; } = [];

    public decimal GetBasePrice()
    {
        return Special == null
            ? throw new NullReferenceException($"{nameof(Special)} was null when calculating Base Price.")
            : Size / (decimal)DefaultSize * Special.BasePrice;
    }

    public decimal GetTotalPrice()
    {
        return Toppings.Any(t => t.Topping is null)
            ? throw new NullReferenceException($"{nameof(Toppings)} contained null when calculating the Total Price.")
            : GetBasePrice() + Toppings.Sum(t => t.Topping!.Price);
    }
}