using AutoMapper;
using PizzaStore.Contracts;
using PizzaStore.Domain;

namespace PizzaStore.Mapper;

public class PizzaStoreMapperProfile : Profile
{
    public PizzaStoreMapperProfile()
    {
        CreateMap<Topping, ToppingDto>().ReverseMap();
        CreateMap<PizzaSpecial, PizzaSpecialDto>().ReverseMap();
        CreateMap<PizzaTopping, PizzaToppingDto>().ReverseMap();
        CreateMap<Pizza, PizzaDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<LatLong, LatLongDto>().ReverseMap();
    }
}