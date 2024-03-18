namespace PizzaStore.Contracts;

public interface IPizzaService
{
    Task<List<PizzaSpecialDto>> GetListSpecialAsync();

    Task<List<ToppingDto>> GetListToppingAsync();

    Task<decimal> GetPizzaBasePriceAsync(OrderPizzaDto orderPizza);

    Task<decimal> GetPizzaTotalPriceAsync(OrderPizzaDto orderPizza);
}