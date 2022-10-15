using BusinessLayer.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class MessageController : Controller
    {

        Message2Manager message2Manager = new Message2Manager(new EfMessage2Repository());
        BlogContext context = new BlogContext();

       
        public IActionResult InBox()
        {
            var userName = User.Identity.Name;
            var userMail = context.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var WriterId = context.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterID).FirstOrDefault();

            
            var values = message2Manager.GetInboxListByWriter(WriterId);
            return View(values);
        }

        public IActionResult SendBox()
        {
            var userName = User.Identity.Name;
            var userMail = context.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var WriterId = context.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterID).FirstOrDefault();


            var values = message2Manager.GetSendboxListByWriter(WriterId);
            return View(values);
        }


        [HttpGet]
        public IActionResult MessageDetails(int id)
        {
            //var context = new BlogContext();
            //burada bir hata var mesaj gönderenin ismini getiremiyorum onu hallet
            //var name = context.Message2s.Where(x => x.SenderID==id).Select(y => y.SenderUser.WriterName).FirstOrDefault();
            //ViewBag.SenderName = name;
            var value = message2Manager.GetById(id);
            return View(value);
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            GetCategorySelectListItem();
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message2 message2)
        {
            var userName = User.Identity.Name;
            var userMail = context.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var WriterId = context.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterID).FirstOrDefault();
            message2.SenderID = WriterId;
            message2.ReceiverID = 2;
            message2.MessageStatus = true;
            message2.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            message2Manager.TAdd(message2);

            return RedirectToAction("Index","Dashboard");
        }

        public  void GetCategorySelectListItem()
        {
  

            UserManager cm = new UserManager(new EfUserRepository());
            List<SelectListItem> recieverUsers = (from x in  cm.GetReceiverUsersList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Email.ToString(),
                                                      Value = x.Id.ToString()
                                                  }).ToList();
            ViewBag.RecieverUser = recieverUsers;
        }


    }
}
