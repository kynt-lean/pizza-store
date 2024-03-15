using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Contracts;
using PizzaStore.Data;

namespace PizzaStore.Services;

public class PizzaService : IPizzaService
{
    private readonly PizzaStoreDbContext Db;
    private readonly IMapper Mapper;

    public PizzaService(PizzaStoreDbContext db, IMapper mapper)
    {
        Db = db;
        Mapper = mapper;
    }

    public async Task<List<PizzaSpecialDto>> GetListSpecialAsync()
    {
        return Mapper.Map<List<PizzaSpecialDto>>(await Db.Specials.ToListAsync());
    }

    public async Task<List<ToppingDto>> GetListToppingAsync()
    {
        return Mapper.Map<List<ToppingDto>>(await Db.Toppings.OrderBy(t => t.Name).ToListAsync());
    }
}