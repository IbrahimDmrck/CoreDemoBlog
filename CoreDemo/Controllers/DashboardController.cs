using DataAccess.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class DashboardController : Controller
    {
      
        public IActionResult Index()
        {
            BlogContext context = new BlogContext();
            ViewBag.AllBlogCount= context.Blogs.Count().ToString();
            ViewBag.MyBlogCount = context.Blogs.Where(x=>x.WriterID==1).Count();
            ViewBag.AllCategories = context.Categories.Count();
            return View();
        }
    }
}
