using CoreDemo.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]    public class WriterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WriterList()
        {//burada da model de oluşturduğumuz classdan veri çekiyoruz ama bunu json formatında çekiyoruz dönerkende bunu json olarak döndürüyoruz
            var jsonWriters = JsonConvert.SerializeObject(writers);
            return Json(jsonWriters);
        }

        [HttpPost]
        public IActionResult AddWriter(WriterClass writer)
        {
            writers.Add(writer);
            var jsonWriter = JsonConvert.SerializeObject(writer);
            return Json(jsonWriter);
        }

        [HttpPost]
        public IActionResult DeleteWriter(int id)
        {
            var writer = writers.FirstOrDefault(x => x.ID == id);
            writers.Remove(writer);     
            return Json(writer);
        }


        [HttpPost]
        public IActionResult UpdateWriter(WriterClass writer)
        {
            var getwriter = writers.FirstOrDefault(x => x.ID == writer.ID);
            getwriter.Name = writer.Name;
            var jsonWriter = JsonConvert.SerializeObject(writer);
            return Json(jsonWriter);
        }



        public IActionResult GetWriterByID(int writerId)
        {
            var findWriter = writers.FirstOrDefault(x => x.ID == writerId);
            var jsonWriters = JsonConvert.SerializeObject(findWriter);
            return Json(jsonWriters);
        }

        public static List<WriterClass> writers = new List<WriterClass>
        {           
               new WriterClass{ID=1,Name="Mehmet"},
               new WriterClass{ID=2,Name="Ahmet"},
               new WriterClass{ID=3,Name="Fatma"},
               new WriterClass{ID=4,Name="Selin"},
               new WriterClass{ID=5,Name="Selma"}
        };
    }
}
