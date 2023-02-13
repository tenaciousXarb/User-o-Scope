using AutoMapper;
using BLL.DTO;
using DAL;
using DAL.EF;
using DAL.Interfaces;
using Newtonsoft.Json.Linq;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;

        public UserService(IMapper mapper, IUserRepository userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }


        public async Task<UserDTO?> AddUser(UserCreationDTO obj)
        {
            var data = _mapper.Map<User>(obj);
            var rt = await _userRepo.Add(data);
            if(rt != null)
            {
                return _mapper.Map<UserDTO>(rt);
            }
            throw new NullReferenceException("Unable to add user");
        }


        public async Task<List<UserDTO>?> Get()
        {
            var data = await _userRepo.Get();
            return _mapper.Map<List<UserDTO>>(data);
        }


        public async Task<List<UserDTO>?> GetAllPagination(int userPerPage, int pageNo = 1)
        {
            #region pagination in service
            /*var data = await _userRepo.Get();

                int userPerPage = 2;
                int pageNo = 2;
                int indexOfLastUser = 2;
                int indexOfFirstUser = 2;
                //List<User> subData = data.GetRange(indexOfFirstUser, indexOfLastUser);
                List<User> subData = data.Skip(userPerPage*(pageNo-1)).Take(userPerPage).ToList();
                return _mapper.Map<List<UserDTO>>(subData);*/ 
            #endregion

            var data = await _userRepo.GetByPagination(userPerPage, pageNo);
            return _mapper.Map<List<UserDTO>>(data);
        }


        public async Task<UserDTO?> Get(int id)
        {
            var data = await _userRepo.Get(id);
            return _mapper.Map<UserDTO>(data);
        }


        public async Task<UserDTO?> Edit(int id, UserCreationDTO obj)
        {
            var data = _mapper.Map<User>(obj);
            var rt = await _userRepo.Update(id, data);
            if (rt != null)
            {
                return _mapper.Map<UserDTO>(rt);
            }
            throw new NullReferenceException("Values not updated");
        }


        public async Task<bool> Delete(int id)
        {
            return await _userRepo.Delete(id);
        }
    }
}
