using DataAccess.Abstract;
using DataAccess.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBlogRepository : GenericRepository<Blog>, IBlogDal
    {
        public List<Blog> GetListWithCategory()
        {
            using (var context= new BlogContext())
            {
                return context.Blogs.Include(p => p.Category).ToList();
                /* biz zaten entityleri tanımlarken gerekli foregin keyleri belirmiştik şimdi o foregin keyler üzerinden verileri 
                 getirebilmek için include metodunu kullandık
                yani burada dedik ki bloglar içinde kategorileri dahil et*/
            }
        }

        public List<Blog> GetListWithCategoryByWriter(int id)
        {
            using (var context = new BlogContext())
            {
                return context.Blogs.Include(p => p.Category).Where(x=>x.WriterID==id).ToList();
            }
        }
    }
}
