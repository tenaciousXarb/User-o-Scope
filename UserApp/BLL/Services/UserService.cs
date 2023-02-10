using AutoMapper;
using BLL.DTO;
using DAL;
using DAL.EF;
using Newtonsoft.Json.Linq;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }


        public async Task<UserDTO?> AddUser(UserCreationDTO obj)
        {
            var data = _mapper.Map<User>(obj);
            var rt = await DataAccessFactory.UserDataAccess().Add(data);
            if(rt != null)
            {
                return _mapper.Map<UserDTO>(rt);
            }
            throw new NullReferenceException("Unable to add user");
        }


        public async Task<List<UserDTO>?> Get()
        {
            var data = await DataAccessFactory.UserDataAccess().Get();
            return _mapper.Map<List<UserDTO>>(data);
        }


        public async Task<List<UserDTO>?> GetAllPagination(int userPerPage, int pageNo = 1)
        {
            #region pagination in service
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


        public async Task<UserDTO?> Edit(int id, UserCreationDTO obj)
        {
            var data = _mapper.Map<User>(obj);
            var rt = await DataAccessFactory.UserDataAccess().Update(id, data);
            if (rt != null)
            {
                return _mapper.Map<UserDTO>(rt);
            }
            throw new NullReferenceException("Values not updated");
        }


        public async Task<bool> Delete(int id)
        {
            return await DataAccessFactory.UserDataAccess().Delete(id);
        }
    }
}
