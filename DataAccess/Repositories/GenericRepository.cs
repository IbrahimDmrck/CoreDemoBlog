using DataAccess.Abstract;
using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        public void Delete(T t)
        {
            using BlogContext context = new BlogContext();
            context.Remove(t);
            context.SaveChanges();
        }

        public T GetById(int id)
        {
            using BlogContext context = new BlogContext();
            return context.Set<T>().Find(id);
        }

        public List<T> GetListAll()
        {
            using BlogContext context = new BlogContext();
            return context.Set<T>().ToList();

        }

        public void Insert(T t)
        {
            using BlogContext context = new BlogContext();
            context.Add(t);
            context.SaveChanges();
        }

        public List<T> GetListAll(Expression<Func<T, bool>> filter)
        {
            using BlogContext context = new BlogContext();
            return context.Set<T>().Where(filter).ToList();
        }

        public void Update(T t)
        {
            using BlogContext context = new BlogContext();
            context.Update(t);
            context.SaveChanges();
        }
    }
}
