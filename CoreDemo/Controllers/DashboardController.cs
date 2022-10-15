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

            var userName = User.Identity.Name;
            var userMail = context.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerId = context.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterID).FirstOrDefault();
            ViewBag.AllBlogCount= context.Blogs.Count().ToString();
            ViewBag.MyBlogCount = context.Blogs.Where(x=>x.WriterID==writerId).Count();
            ViewBag.AllCategories = context.Categories.Count();
            return View();
        }
    }
}
