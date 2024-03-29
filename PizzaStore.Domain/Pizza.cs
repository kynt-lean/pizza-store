namespace PizzaStore.Domain;

/// <summary>
///    /// Represents a customized pizza as part of an order
/// </summary>
public class Pizza
{
    public const int DefaultSize = 12;

    public int Id { get; set; }

    public int OrderId { get; set; }

    public PizzaSpecial? Special { get; set; }

    public int SpecialId { get; set; }

    public int Size { get; set; }

    public List<PizzaTopping> Toppings { get; set; } = [];

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