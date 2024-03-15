using AutoMapper;
using PizzaStore.Contracts;
using PizzaStore.Domain;

namespace PizzaStore.Mapper;

public class PizzaStoreMapperProfile : Profile
{
    public PizzaStoreMapperProfile()
    {
        CreateMap<Topping, ToppingDto>();
        CreateMap<PizzaSpecial, PizzaSpecialDto>();
        CreateMap<PizzaTopping, PizzaToppingDto>();
        CreateMap<Pizza, PizzaDto>()
            .ForMember(dest => dest.BasePrice, opts => opts.MapFrom(src => src.GetBasePrice()))
            .ForMember(dest => dest.TotalPrice, opts => opts.MapFrom(src => src.GetTotalPrice()));
    }
}