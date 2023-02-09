using AutoMapper;
using BLL.DTO;
using DAL;
using DAL.EF;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        public async Task<UserDTO?> AddUser(UserDTO obj)
        {
            var cfg = new MapperConfiguration(c => {
                c.CreateMap<UserDTO, User>();
                c.CreateMap<User, UserDTO>();
            });
            var mapper = new Mapper(cfg);
            var data = mapper.Map<User>(obj);
            var rt = await DataAccessFactory.UserDataAccess().Add(data);
            return mapper.Map<UserDTO>(rt);
        }
        public async Task<List<UserDTO>?> Get()
        {
            var data = await DataAccessFactory.UserDataAccess().Get();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<User, UserDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<List<UserDTO>>(data);
        }
        public async Task<UserDTO?> Get(int id)
        {
            var data = await DataAccessFactory.UserDataAccess().Get(id);
            var cfg = new MapperConfiguration(c => {
                c.CreateMap<User, UserDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<UserDTO>(data);
        }
        public async Task<UserDTO?> Edit(UserDTO obj)
        {
            var cfg = new MapperConfiguration(c => {
                c.CreateMap<UserDTO, User>();
                c.CreateMap<User, UserDTO>();
            });
            var mapper = new Mapper(cfg);
            var data = mapper.Map<User>(obj);
            var rt = await DataAccessFactory.UserDataAccess().Update(data);
            return mapper.Map<UserDTO>(rt);
        }
        public async Task<bool> Delete(int id)
        {
            return await DataAccessFactory.UserDataAccess().Delete(id);
        }
    }
}
