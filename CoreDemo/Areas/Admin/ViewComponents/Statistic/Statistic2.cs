using DataAccess.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic2 : ViewComponent
    {

        BlogContext context = new BlogContext();
        public IViewComponentResult Invoke()
        {
            ViewBag.AdminName = context.Admins.Where(x => x.AdminID == 1).Select(x => x.Name).FirstOrDefault();
            ViewBag.AdminDescription = context.Admins.Where(x => x.AdminID == 1).Select(x => x.ShortDescription).FirstOrDefault();
            ViewBag.AdminImg = context.Admins.Where(x => x.AdminID == 1).Select(x => x.ImageURL).FirstOrDefault();
            return View();
        }
    }
}
