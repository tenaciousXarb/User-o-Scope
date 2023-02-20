using AppUser.DataAccess.AppData;
using AppUser.DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AppUser.DataAccess.Repositories
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : BaseEntity
    {
        #region fields
        private readonly UserProjectDbContext _context;
        #endregion


        #region ctor
        public EntityBaseRepository(UserProjectDbContext context)
        {
            _context = context;
        }
        #endregion


        #region add async (DB)
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }
        #endregion


        #region update async (DB)
        public async Task<T> UpdateAsync(T newEntity)
        {
            var oldEntity = _context.Set<T>().Find((newEntity.Id));
            _context.Set<T>().Entry(oldEntity).CurrentValues.SetValues(newEntity);
            await SaveChangesAsync();
            return newEntity;
        }
        #endregion


        #region delete async (DB)
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _context.Set<T>().Remove(entity);
            return await SaveChangesAsync();
        }
        #endregion


        #region get async (DB)
        public async Task<List<T>> GetAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        #endregion


        #region get by ID async (DB)
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        #endregion


        #region save changes async (DB)
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
        #endregion
    }
}
