using BusinessLayer.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

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

            string api = "53c9a89b46fb2bcee8bbb8781789a6c4";
            string connection = "https://api.openweathermap.org/data/2.5/weather?q=Yalova&mode=xml&lang=tr&units=metric&appid="+api;
            XDocument document = XDocument.Load(connection);

            ViewBag.Weather = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;

            ViewBag.WeatherStatus = document.Descendants("weather").ElementAt(0).Attribute("value").Value;

            return View();
        }
    }
}
