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
        public Task<UserDTO?> AddUser(UserDTO obj);
        public Task<List<UserDTO>?> Get();
        public Task<UserDTO?> Get(int id);
        public Task<UserDTO?> Edit(UserDTO obj);
        public Task<bool> Delete(int id);
    }
}
