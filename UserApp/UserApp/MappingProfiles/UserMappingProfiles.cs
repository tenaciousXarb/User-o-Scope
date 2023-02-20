using AppUser.BusinessServices.DTO;
using AppUser.DataAccess.AppData;
using AutoMapper;

namespace AppUser.API.MappingProfiles
{
    /// <summary>
    /// UserMappingProfiles
    /// </summary>
    public class UserMappingProfiles : Profile
    {
        /// <summary>
        /// UserMappingProfiles Constructor
        /// </summary>
        public UserMappingProfiles()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserCreationDTO>().ReverseMap();
        }
    }
}
