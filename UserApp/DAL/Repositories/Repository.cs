using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class Repository
    {
        protected readonly UserProjectDbContext db;
        public Repository()
        {
            db = new UserProjectDbContext();
        }
    }
}
