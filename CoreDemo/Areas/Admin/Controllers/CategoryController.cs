using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccess.Concrete.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace CoreDemo.Areas.Admin.Controllers
{
    //oluşturduğumuz areada çağırılan controllerin çalışa bilmesi için area ttiributunu almaları gerekmektedir
    //area nedir araştır
    [Area("Admin")]
    public class CategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());

        public IActionResult Index(int page= 1)
        {
            var values = categoryManager.GetList().ToPagedList(page,3);
            return View(values);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {

            CategoryValidator blogValidator = new CategoryValidator();
            ValidationResult result = blogValidator.Validate(category);

            if (result.IsValid)
            {

                category.CategoryStatus = true;
              
                categoryManager.TAdd(category);
                return RedirectToAction("Index", "Category");
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

        public IActionResult CategoryDelete(int id)
        {
            var CategoryValue = categoryManager.GetById(id);
            categoryManager.TDelete(CategoryValue);
            return RedirectToAction("Index");
        }
    }
}
