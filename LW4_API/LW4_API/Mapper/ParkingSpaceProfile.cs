using LW4_API.Model.DTO;
using LW4_API.Model.Entity;
using AutoMapper;
namespace LW4_API.Mapper
{
    public class ParkingSpaceProfile: Profile
    {
        public ParkingSpaceProfile()
        {
            CreateMap<ParkingSpaceDTO, ParkingSpace>().ReverseMap();

        }
    }
}
