using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Contracts;
using PizzaStore.Data;
using PizzaStore.Domain;

namespace PizzaStore.Services;

public class PizzaService : IPizzaService
{
    private readonly PizzaStoreDbContext _db;
    private readonly IMapper _mapper;

    public PizzaService(PizzaStoreDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<List<PizzaSpecialDto>> GetListSpecialAsync()
    {
        return _mapper.Map<List<PizzaSpecialDto>>(await _db.Specials.ToListAsync());
    }

    public async Task<List<ToppingDto>> GetListToppingAsync()
    {
        return _mapper.Map<List<ToppingDto>>(await _db.Toppings.OrderBy(t => t.Name).ToListAsync());
    }

    public Task<decimal> GetPizzaBasePriceAsync(OrderPizzaDto orderPizza)
    {
        var pizza = _mapper.Map<Pizza>(orderPizza);
        return Task.FromResult(pizza.GetBasePrice());
    }

    public Task<decimal> GetPizzaTotalPriceAsync(OrderPizzaDto orderPizza)
    {
        var pizza = _mapper.Map<Pizza>(orderPizza);
        return Task.FromResult(pizza.GetTotalPrice());
    }
}