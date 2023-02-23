using AppUser.BusinessServices.DTO;
using AppUser.DataAccess.AppData;
using AutoMapper;

namespace AppUser.API.MappingProfiles
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
