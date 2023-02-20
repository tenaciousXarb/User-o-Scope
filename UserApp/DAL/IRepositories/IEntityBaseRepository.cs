using AppUser.DataAccess.AppData;

namespace AppUser.DataAccess.IRepositories
{
    public interface IEntityBaseRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<List<T>> GetAsync();
        Task<T?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
