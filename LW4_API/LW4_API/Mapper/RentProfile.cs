using LW4_API.Model.DTO;
using LW4_API.Model.Entity;
using LW4_API.Model.Domain;
using LW4_API.Model.View;
using AutoMapper;

namespace LW4_API.Mapper
{
    public class RentProfile :Profile
    {
        public RentProfile()
        {
            CreateMap<RentDTO, RentDomain>().ReverseMap();
            CreateMap<RentDomain, Rent>().ReverseMap();
            CreateMap<Rent, RentView>()
                .ForMember(dest => dest.ClientName,
                           opt => opt.Ignore())   // заповнюватимемо вручну через сервіс
                .ForMember(dest => dest.ClientSurname,
                           opt => opt.Ignore())
                .ForMember(dest => dest.ParkingSpaceId,
                           opt => opt.MapFrom(src => src.ParkingSpaceId))
                .ForMember(dest => dest.IsVip,
                           opt => opt.Ignore())   // теж через сервіс
                .ForMember(dest => dest.PriceForHour,
                           opt => opt.Ignore())
                .ForMember(dest => dest.Price,
                           opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.RentStart,
                           opt => opt.MapFrom(src => src.RentStart))
                .ForMember(dest => dest.RentEnd,
                           opt => opt.MapFrom(src => src.RentEnd));
        }
    }
}
