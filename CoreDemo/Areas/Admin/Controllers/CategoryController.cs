using BusinessLayer.Concrete;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace CoreDemo.Areas.Admin.Controllers
{
    //oluşturduğumuz areada çağırılan controllerin çalışa bilmesi için area ttiributunu almaları gerekmektedir
    //area nedir araştır
    [Area("Admin")]
    public class CategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());

        public IActionResult Index(int page= 1)
        {
            var values = categoryManager.GetList().ToPagedList(page,3);
            return View(values);
        }
    }
}
