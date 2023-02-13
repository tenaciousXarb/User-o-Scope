using DAL.EF;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class UserRepository : Repository, IUserRepo
    {
        private readonly UserProjectDbContext _userProjectDbContext;
        public UserRepository(UserProjectDbContext userProjectDbContext)
        {
            _userProjectDbContext = userProjectDbContext;
        }

        public async Task<User?> Add(User user)
        {
            await _userProjectDbContext.Users.AddAsync(user);
            if (await _userProjectDbContext.SaveChangesAsync() > 0)
            {
                return user;
            }
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await Get(id);
            if (obj != null)
            {
                _userProjectDbContext.Users.Remove(obj);
                return await _userProjectDbContext.SaveChangesAsync()>0;
            }
            throw new KeyNotFoundException("User not found");
        }

        public async Task<List<User>?> Get()
        {
            return await _userProjectDbContext.Users.ToListAsync();
        }

        public async Task<User?> Get(int id)
        {
            return await _userProjectDbContext.Users.FindAsync(id);
        }

        public async Task<List<User>?> GetByPagination(int userPerPage, int pageNumber)
        {
            return await _userProjectDbContext.Users
                .Skip(userPerPage * (pageNumber - 1))
                .Take(userPerPage)
                .ToListAsync();
        }

        public async Task<User?> Update(int id, User obj)
        {
            var _userProjectDbContextpost = _userProjectDbContext.Users.Find(id);
            if(_userProjectDbContextpost != null)
            {
                //_userProjectDbContext.Entry(_userProjectDbContextpost).CurrentValues.SetValues(obj);
                _userProjectDbContextpost.Name = obj.Name;
                _userProjectDbContextpost.Password = obj.Password;
                _userProjectDbContextpost.Email = obj.Email;
                _userProjectDbContextpost.Address = obj.Address;
                await _userProjectDbContext.SaveChangesAsync();
                return _userProjectDbContextpost;
            }
            throw new KeyNotFoundException("User not found");
        }

        public async Task<User?> Authenticate(string email, string pass)
        {
            return await _userProjectDbContext.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == pass);
        }
    }
}
