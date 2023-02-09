using AutoMapper;

namespace BLL.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        CreateMap<UserDTO, User>();
        CreateMap<User, UserDTO>();
    }
}
