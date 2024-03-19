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
}