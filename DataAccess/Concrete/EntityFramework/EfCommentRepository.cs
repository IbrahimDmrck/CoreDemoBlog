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
    public class EfCommentRepository : GenericRepository<Comment>, ICommentDal
    {
        public List<Comment> GetListWithCommentByBlog()
        {
            using (var context = new BlogContext())
            {
                return context.Comments.Include(x => x.Blog).ToList();
            }
          
        }
    }
}
