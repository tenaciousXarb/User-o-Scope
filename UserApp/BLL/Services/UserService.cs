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
        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }


        {
            var data = _mapper.Map<User>(obj);
            var rt = await DataAccessFactory.UserDataAccess().Add(data);
            return mapper.Map<UserDTO>(rt);
                return _mapper.Map<UserDTO>(rt);
        }
        public async Task<List<UserDTO>?> Get()
        {
            var data = await DataAccessFactory.UserDataAccess().Get();
            return _mapper.Map<List<UserDTO>>(data);
        }
        public async Task<List<UserDTO>?> GetAllPagination(int userPerPage, int pageNo)
        {
            /*var data = await DataAccessFactory.UserDataAccess().Get();

                int userPerPage = 2;
                int pageNo = 2;
                int indexOfLastUser = 2;
                int indexOfFirstUser = 2;
                //List<User> subData = data.GetRange(indexOfFirstUser, indexOfLastUser);
                List<User> subData = data.Skip(userPerPage*(pageNo-1)).Take(userPerPage).ToList();
                return _mapper.Map<List<UserDTO>>(subData);*/ 
            #endregion

            var data = await DataAccessFactory.UserDataAccess().GetByPagination(userPerPage, pageNo);
            return _mapper.Map<List<UserDTO>>(data);
        }


        public async Task<UserDTO?> Get(int id)
        {
            var data = await DataAccessFactory.UserDataAccess().Get(id);
            return _mapper.Map<UserDTO>(data);
        }
        public async Task<UserDTO?> Edit(UserDTO obj)
        {
            var data = _mapper.Map<User>(obj);
                return _mapper.Map<UserDTO>(rt);
        }
        public async Task<bool> Delete(int id)
        {
            return await DataAccessFactory.UserDataAccess().Delete(id);
        }
    }
}
