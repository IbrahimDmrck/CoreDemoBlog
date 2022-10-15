using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class WriterController : Controller
    {
        WriterManager writrManager = new WriterManager(new EfWriterRepository());

        private readonly UserManager<AppUser> _userManager;

        public WriterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        BlogContext context = new BlogContext();


        [Authorize]
        public IActionResult Index()
        {
            var userMail = User.Identity.Name;
            ViewBag.UserMail = userMail;

            var WriterName = context.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterName).FirstOrDefault();
            ViewBag.Writer = WriterName;
            return View();
        }

        [AllowAnonymous]
        public IActionResult Test()
        {
            return View();
        }

        [AllowAnonymous]
        public PartialViewResult WriterNavbarPartial()
        {
            return PartialView();
        }

        [AllowAnonymous]
        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> WriterEditProfile()
        {
            var userName = User.Identity.Name;
            var userMail = context.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            //var WriterId = context.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterID).FirstOrDefault();
            //var values = writrManager.GetById(WriterId);
            //return View(values);
            UserManager userManager = new UserManager(new EfUserRepository());
            var ID = context.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            var values = userManager.GetById(ID);
           // var values = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(values);
        }


        [HttpPost]
        public IActionResult WriterEditProfile(Writer writer, AddProfileImage writerProfileImage)
        {
            WriterValidator writerValidator = new WriterValidator();
            ValidationResult result = writerValidator.Validate(writer);

            if (result.IsValid)
            {
                writrManager.TUpdate(writer);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult WriterAdd()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult WriterAdd(AddProfileImage writerProfileImage)
        {
            Writer writer = new Writer();
            if (writerProfileImage.WriterImage != null)
            {
                var extension = Path.GetExtension(writerProfileImage.WriterImage.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                writerProfileImage.WriterImage.CopyTo(stream);
                writer.WriterImage = newImageName;

            }
            writer.WriterMail = writerProfileImage.WriterMail;
            writer.WriterName = writerProfileImage.WriterName;
            writer.WriterPassword = writerProfileImage.WriterPassword;
            writer.WriterStatus = true;
            writer.WriterAbout = writerProfileImage.WriterAbout;

            writrManager.TAdd(writer);
            return RedirectToAction("Index", "Dashboard");
        }

    }
}
