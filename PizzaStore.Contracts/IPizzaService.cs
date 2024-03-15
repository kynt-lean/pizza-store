namespace PizzaStore.Contracts;

public interface IPizzaService
{
    Task<List<PizzaSpecialDto>> GetListSpecialAsync();

    Task<List<ToppingDto>> GetListToppingAsync();
}