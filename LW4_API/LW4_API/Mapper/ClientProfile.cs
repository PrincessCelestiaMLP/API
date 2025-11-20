using AutoMapper;
using LW4_API.Model.DTO;
using LW4_API.Model.Entity;
namespace LW4_API.Mapper
{
    public class ClientProfile : Profile
    {

        public ClientProfile()
        {
            CreateMap<ClientDTO, Client>().ReverseMap();
           
        }
    }
}
