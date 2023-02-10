using AutoMapper;
using BLL.DTO;
using DAL.EF;

namespace AppUser.MappingProfiles
{
    public class UserMappingProfiles : Profile
    {
        public UserMappingProfiles()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserCreationDTO>().ReverseMap();
        }
    }
}
