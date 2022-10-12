using BusinessLayer.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.ViewComponents.Writer
{
    public class WriterAboutOnDashboard : ViewComponent
    {
        WriterManager writerManager = new WriterManager(new EfWriterRepository());
        BlogContext context = new BlogContext();

        public IViewComponentResult Invoke()
        {
            
            var userName = User.Identity.Name;
            ViewBag.UserName = userName;

            var userMail = context.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            ViewBag.UserMail = userMail;
            var WriterId = context.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterID).FirstOrDefault();   
            var values = writerManager.GetWriterById(WriterId);
            return View(values);
        }
    }
}
