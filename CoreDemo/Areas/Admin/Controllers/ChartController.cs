using CoreDemo.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StaticCategoryChart()
        {
            List<CategoryClass> chartList = new List<CategoryClass>();

            chartList.Add(new CategoryClass { CategoryCount = 78, CategoryName = "Teknoloji" });
            chartList.Add(new CategoryClass { CategoryCount = 30, CategoryName = "Yazılım" });
            chartList.Add(new CategoryClass { CategoryCount = 56, CategoryName = "Spor" });
            chartList.Add(new CategoryClass { CategoryCount = 98, CategoryName = "Sağlık" });
            chartList.Add(new CategoryClass { CategoryCount = 65, CategoryName = "Medya" });

            return Json(new { jsonList=chartList});
        }
    }
}
