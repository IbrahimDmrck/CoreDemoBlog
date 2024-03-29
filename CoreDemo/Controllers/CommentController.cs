﻿using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccess.Concrete.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult PartialAddComment()
        {
            return PartialView();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult PartialAddComment(Comment comment,int blogID)
        {
            comment.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            comment.CommentStatus = true;
            comment.BlogID = blogID;
            _commentService.CommentAdd(comment);
            return Ok();
        }

        public PartialViewResult CommentListByBlog(int id)
        {
            var values = _commentService.GetListById(id);
            return PartialView(values);
        }
    }
}
