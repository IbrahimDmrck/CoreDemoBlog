using BusinessLayer.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic1 : ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());
        BlogContext context = new BlogContext();
        public IViewComponentResult Invoke()
        {
            ViewBag.BlogCount = blogManager.GetList().Count();
            ViewBag.ContactCount = context.Contacts.Count();
            ViewBag.CommentCount = context.Comments.Count();
            ViewBag.LastBLog = context.Blogs.OrderByDescending(x => x.BlogID).Select(x => x.BlogTitle).Take(1).FirstOrDefault();
            return View();
        }
    }
}
