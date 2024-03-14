namespace PizzaStore.Domain;

public class PizzaTopping
{
    public Topping? Topping { get; set; }

    public int ToppingId { get; set; }

    public int PizzaStoreId { get; set; }
}