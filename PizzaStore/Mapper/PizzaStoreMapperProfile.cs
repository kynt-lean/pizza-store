using AutoMapper;
using PizzaStore.Contracts;
using PizzaStore.Domain;

namespace PizzaStore.Mapper;

public class PizzaStoreMapperProfile : Profile
{
    public PizzaStoreMapperProfile()
    {
        CreateMap<Topping, ToppingDto>();
        CreateMap<PizzaSpecial, PizzaSpecialDto>().ReverseMap();
        CreateMap<PizzaTopping, PizzaToppingDto>().ReverseMap();
        CreateMap<OrderPizzaDto, Pizza>();
    }
}