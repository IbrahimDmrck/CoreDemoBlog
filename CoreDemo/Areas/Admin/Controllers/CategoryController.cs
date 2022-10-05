using BusinessLayer.Concrete;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.Controllers
{
    //oluşturduğumuz areada çağırılan controllerin çalışa bilmesi için area ttiributunu almaları gerekmektedir
    //area nedir araştır
    [Area("Admin")]
    public class CategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());

        public IActionResult Index()
        {
            var values = categoryManager.GetList();
            return View(values);
        }
    }
}
