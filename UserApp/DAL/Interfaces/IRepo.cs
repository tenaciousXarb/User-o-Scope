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
        Task<RET?> Update(CLASS obj);
        Task<List<CLASS>?> Get();
        Task<CLASS?> Get(ID id);
        Task<bool> Delete(ID id);
    }
}
