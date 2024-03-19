using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Contracts;
using PizzaStore.Data;

namespace PizzaStore.Services;

public class PizzaService(PizzaStoreDbContext db, IMapper mapper) : IPizzaService
{
    private readonly PizzaStoreDbContext _db = db;
    private readonly IMapper _mapper = mapper;

    public async Task<List<PizzaSpecialDto>> GetListSpecialAsync()
    {
        return _mapper.Map<List<PizzaSpecialDto>>(await _db.Specials.ToListAsync());
    }

    public async Task<List<ToppingDto>> GetListToppingAsync()
    {
        return _mapper.Map<List<ToppingDto>>(await _db.Toppings.OrderBy(t => t.Name).ToListAsync());
    }
}