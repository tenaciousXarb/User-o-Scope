using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepo<CLASS, ID, RET>
    {
        Task<RET?> Add(CLASS obj);
        Task<RET?> Update(ID id, CLASS obj);
        Task<List<CLASS>?> Get();
        Task<CLASS?> Get(ID id);
        Task<bool> Delete(ID id);
        Task<List<CLASS>?> GetByPagination(int userPerPage, int pageNumber);
    }
}
