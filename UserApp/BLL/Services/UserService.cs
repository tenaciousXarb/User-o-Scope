using AutoMapper;
using BLL.DTO;
using DAL;
using DAL.EF;
using Newtonsoft.Json.Linq;

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
        public async Task<List<UserDTO>?> GetAllPagination(int userPerPage, int pageNo)
        {
            /*var data = await DataAccessFactory.UserDataAccess().Get();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<User, UserDTO>();
            });
            var mapper = new Mapper(cfg);
            int userPerPage = 2;
            int pageNo = 2;
            int indexOfLastUser = 2;
            int indexOfFirstUser = 2;
            //List<User> subData = data.GetRange(indexOfFirstUser, indexOfLastUser);
            List<User> subData = data.Skip(userPerPage*(pageNo-1)).Take(userPerPage).ToList();
            return mapper.Map<List<UserDTO>>(subData);*/

            var data = await DataAccessFactory.UserDataAccess().GetByPagination(userPerPage, pageNo);
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
