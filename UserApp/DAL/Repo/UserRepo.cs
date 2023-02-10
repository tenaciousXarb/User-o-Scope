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
    internal class UserRepo : Repo, IRepo<User, int, User>, IAuth<User>
    {
        public async Task<User?> Add(User obj)
        {
            await db.Users.AddAsync(obj);
            if (await db.SaveChangesAsync() > 0)
            {
                return obj;
            }
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await Get(id);
            if (obj != null)
            {
                db.Users.Remove(obj);
                return await db.SaveChangesAsync()>0;
            }
            throw new KeyNotFoundException("User not found");
        }

        public async Task<List<User>?> Get()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<User?> Get(int id)
        {
            return await db.Users.FindAsync(id);
        }

        public async Task<List<User>?> GetByPagination(int userPerPage, int pageNumber)
        {
            return await db.Users
                .Skip(userPerPage * (pageNumber - 1))
                .Take(userPerPage)
                .ToListAsync();
        }

        public async Task<User?> Update(int id, User obj)
        {
            var dbpost = db.Users.Find(id);
            if(dbpost != null)
            {
                //db.Entry(dbpost).CurrentValues.SetValues(obj);
                dbpost.Name = obj.Name;
                dbpost.Password = obj.Password;
                dbpost.Email = obj.Email;
                dbpost.Address = obj.Address;
                await db.SaveChangesAsync();
                return dbpost;
            }
            throw new KeyNotFoundException("User not found");
        }

        public async Task<User?> Authenticate(string email, string pass)
        {
            return await db.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == pass);
        }
    }
}
