namespace PizzaStore.Contracts;

public interface IPizzaService
{
    Task<List<PizzaSpecialDto>> GetListSpecialAsync();

    Task<List<ToppingDto>> GetListToppingAsync();

    Task<decimal> GetPizzaBasePriceAsync(PizzaDto pizza);

    Task<decimal> GetPizzaTotalPriceAsync(PizzaDto pizza);
}