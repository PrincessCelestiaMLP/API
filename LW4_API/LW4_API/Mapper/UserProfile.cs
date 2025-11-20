using LW4_API.Model.DTO;
using LW4_API.Model.Entity;
using AutoMapper;
namespace LW4_API.Mapper
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();

        }
    }
}
