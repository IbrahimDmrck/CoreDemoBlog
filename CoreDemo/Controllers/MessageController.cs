using BusinessLayer.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class MessageController : Controller
    {

        Message2Manager message2Manager = new Message2Manager(new EfMessage2Repository());

        [AllowAnonymous]
        public IActionResult InBox()
        {
            int id = 1;
            var values = message2Manager.GetInboxListByWriter(id);
            return View(values);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult MessageDetails(int id)
        {
            var context = new BlogContext();
            //burada bir hata var mesaj gönderenin ismini getiremiyorum onu hallet
            var name = context.Message2s.Where(x => x.SenderID==id).Select(y => y.SenderUser.WriterName).FirstOrDefault();
            ViewBag.SenderName = name;
            var value = message2Manager.GetById(id);
            return View(value);
        }
    }
}
