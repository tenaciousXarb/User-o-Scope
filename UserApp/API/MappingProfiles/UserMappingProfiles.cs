using AutoMapper;
using UserApp.BusinessServices.DTO;
using UserApp.DataAccess.AppData;

namespace UserApp.API.MappingProfiles
{
    public class UserMappingProfiles : Profile
    {
        public UserMappingProfiles()
        {
            #region Profiles
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserCreationDTO>().ReverseMap();
            #endregion
        }
    }
}
