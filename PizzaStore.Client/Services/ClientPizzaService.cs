using System.Net.Http.Json;
using PizzaStore.Contracts;

namespace PizzaStore.Client.Services;

public class ClientPizzaService(HttpClient http) : IPizzaService
{
    public async Task<List<PizzaSpecialDto>> GetListSpecialAsync()
    {
        return await http.GetFromJsonAsync<List<PizzaSpecialDto>>("pizza/specials") ?? [];
    }

    public async Task<List<ToppingDto>> GetListToppingAsync()
    {
        return await http.GetFromJsonAsync<List<ToppingDto>>("pizza/toppings") ?? [];
    }

    public async Task<decimal> GetPizzaBasePriceAsync(PizzaDto pizza)
    {
        return Convert.ToDecimal(await (await http.PostAsJsonAsync("pizza/base-price", pizza)).Content.ReadAsStringAsync());
    }

    public async Task<decimal> GetPizzaTotalPriceAsync(PizzaDto pizza)
    {
        return Convert.ToDecimal(await (await http.PostAsJsonAsync("pizza/total-price", pizza)).Content.ReadAsStringAsync());
    }
}