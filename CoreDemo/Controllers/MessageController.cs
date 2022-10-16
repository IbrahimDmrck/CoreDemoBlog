using BusinessLayer.Abstract;
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
        UserManager userManager=new UserManager(new EfUserRepository());

        public MessageController()
        {
            
        }

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
            
            var value = message2Manager.GetById(id);
            return View(value);
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            GetReceiverSelectListItem();
            return View();
        }

        [HttpPost]
        public  IActionResult SendMessage(Message2 message2)
        {
            var userName = User.Identity.Name;
            var userMail = context.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var WriterId = context.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterID).FirstOrDefault();
            
            

            message2.SenderID = WriterId;
            message2.MessageStatus = true;
            message2.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            message2Manager.TAdd(message2);
            GetReceiverSelectListItem();
            return RedirectToAction("Index","Dashboard");
        }

        public  void GetReceiverSelectListItem()
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

        public void GetSenderSelectListItem()
        {


            UserManager cm = new UserManager(new EfUserRepository());
            List<SelectListItem> senderUser = (from x in cm.GetSenderUsersList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Email.ToString(),
                                                      Value = x.Id.ToString()
                                                  }).ToList();
            ViewBag.SenderUser = senderUser;
        }


    }
}
