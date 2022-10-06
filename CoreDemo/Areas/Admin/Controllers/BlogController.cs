using ClosedXML.Excel;
using CoreDemo.Areas.Admin.Models;
using DataAccess.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        /*
         burada yapılan işlem excell de statik bir tblo oluşturaktır
        bunun için ClosedXML.Excel; bu kütüphaneyi ekledik
         */
        public IActionResult ExportStaticExcelBlogList()
        {
            //burada excel dosyamızı oluşturduk
            using (var WorkBook = new XLWorkbook())
            {
                var worksheet = WorkBook.Worksheets.Add("Blog Listesi");//excel dosyasına blog listesi adında bir tablo oluşturduk
                //burada tablomuzun sütünlarını belirledik
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Adı";

                int BlogRowCount = 2;
                foreach (var item in GetBlogList())
                {
                    // burada aşağıda oluşturduğumuz statik ddataları döngü sayesinde gezip tabloda listeledik
                    worksheet.Cell(BlogRowCount, 1).Value = item.ID;
                    worksheet.Cell(BlogRowCount, 2).Value = item.BlogName;
                    BlogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    //burada excel dosyamızıın adını ve yolunu belirledik
                    WorkBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet","Calisma.xlsx");
                }
            }
            
        }


        //bu methotta xcell tablomuzda listelemek için bir kaç veri statik olarak ekledik
        public List<BlogModel> GetBlogList()
        {
            
            List<BlogModel> blogmodel = new List<BlogModel>{
                new BlogModel{ ID=1,BlogName="C# programlamaya giriş"},
                new BlogModel{ ID=2,BlogName="C# programlamaya giriş"},
                new BlogModel{ ID=3,BlogName="C# programlamaya giriş"}

            };
            return blogmodel;
        }

        public IActionResult BlogListExcell()
        {
            return View();
        }

        public IActionResult ExportDynamicExcelBlogList()
        {
            //dinamik olarak veritabanındaki blogları excel dosyasında indiriyoruz
            //burada excel dosyamızı oluşturduk
            using (var WorkBook = new XLWorkbook())
            {
                var worksheet = WorkBook.Worksheets.Add("Blog Listesi");//excel dosyasına blog listesi adında bir tablo oluşturduk
                //burada tablomuzun sütünlarını belirledik
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Adı";

                int BlogRowCount = 2;
                foreach (var item in BlogTitleList())
                {
                    // burada aşağıda oluşturduğumuz statik ddataları döngü sayesinde gezip tabloda listeledik
                    worksheet.Cell(BlogRowCount, 1).Value = item.ID;
                    worksheet.Cell(BlogRowCount, 2).Value = item.BlogName;
                    BlogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    //burada excel dosyamızıın adını ve yolunu belirledik
                    WorkBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Calisma.xlsx");
                }
            }
        }

        public List<BlogModel2> BlogTitleList()
        {
            List<BlogModel2> blogModel2 = new List<BlogModel2>();
            using (var context= new BlogContext())
            {
                blogModel2 = context.Blogs.Select(x => new BlogModel2
                {
                    ID=x.BlogID,
                    BlogName=x.BlogTitle
                }).ToList();
            }
            return blogModel2;
        }

        public IActionResult BlogTitleListExcel()
        {
            return View();
        }
    }
}
