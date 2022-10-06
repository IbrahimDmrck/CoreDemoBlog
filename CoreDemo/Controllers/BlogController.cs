using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace CoreDemo.Controllers
{

    public class BlogController : Controller
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());
        BlogContext context = new BlogContext();

        public IActionResult Index(int page=1)
        {
            var values = blogManager.GetBlogListWithCategory().ToPagedList(page,6);
            return View(values);
        }

        public IActionResult BlogReadAll(int id)
        {
            ViewBag.BlogId = id;
            var values = blogManager.GetBlogById(id);
            return View(values);
        }

        public IActionResult BlogListByWriter()
        {
            var userMail = User.Identity.Name;
            var WriterId = context.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterID).FirstOrDefault();
            var values = blogManager.GetListWithCategoryByWriterBlogManager(WriterId);
            return View(values);
        }

        [HttpGet]
        public IActionResult BlogAdd()
        {
            GetCategorySelectListItem();
            return View();
        }

        public void GetCategorySelectListItem()
        {
            CategoryManager cm = new CategoryManager(new EfCategoryRepository());
            List<SelectListItem> CategoryValues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.CategoryList = CategoryValues;
        }

        [HttpPost]
        public IActionResult BlogAdd(Blog blog)
        {
            var userMail = User.Identity.Name;
            var WriterId = context.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterID).FirstOrDefault();
            BlogValidator blogValidator = new BlogValidator();
            ValidationResult result = blogValidator.Validate(blog);

            if (result.IsValid)
            {

                blog.BlogStatus = true;
                blog.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                blog.WriterID = WriterId;
                blogManager.TAdd(blog);
                return RedirectToAction("BlogListByWriter", "Blog");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            GetCategorySelectListItem();
            return View();
        }

        public IActionResult DeleteBlog(int id)
        {
            var blogValue = blogManager.GetById(id);
            blogManager.TDelete(blogValue);
            return RedirectToAction("BlogListByWriter", "Blog");
        }

        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            var blogValue = blogManager.GetById(id);
            GetCategorySelectListItem();
            return View(blogValue);
        }

        [HttpPost]
        public IActionResult EditBlog(Blog blog)
        {
            var userMail = User.Identity.Name;
            var WriterId = context.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterID).FirstOrDefault();
            blogManager.TUpdate(blog);
            blog.WriterID = WriterId;
            return RedirectToAction("BlogListByWriter","Blog");
        }
    }
}
