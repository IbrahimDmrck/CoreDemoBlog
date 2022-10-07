using DataAccess.Abstract;
using DataAccess.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAdminRepository : GenericRepository<Admin>, IAdminDal

    {
    }
}
