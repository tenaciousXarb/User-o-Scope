using AutoMapper;
using BLL.DTO;
using DAL.EF;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IUserService
    {
        public Task<UserDTO?> AddUser(UserCreationDTO obj);
        public Task<List<UserDTO>?> Get();
        public Task<List<UserDTO>?> GetAllPagination(int userPerPage, int pageNo);
        public Task<UserDTO?> Get(int id);
        public Task<UserDTO?> Edit(int id, UserCreationDTO obj);
        public Task<bool> Delete(int id);
    }
}
