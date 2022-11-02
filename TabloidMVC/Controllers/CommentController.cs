﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Repositories;
using System.Linq;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        //private readonly Post _post;
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            //_post = post;
        }

        public IActionResult Index(int id)
        {
            //get comments and sort by date - most recent first
            List<Comment> comments = _commentRepository.GetPostsComments(id).OrderByDescending(x => x.CreateDateTime).ToList();
            return View(comments);
        }

        //details and create methods commented out for now

        //public IActionResult Details(int id)
        //{
        //    var comment = _commentRepository.GetPublishedCommentById(id);
        //    if (comment == null)
        //    {
        //        int userId = GetCurrentUserProfileId();
        //        comment = _commentRepository.GetUserCommentById(id, userId);
        //        if (comment == null)
        //        {
        //            return NotFound();
        //        }
        //    }
        //    return View(comment);
        //}

        //public IActionResult Create()
        //{
        //    var vm = new CommentCreateViewModel();
        //    vm.CategoryOptions = _commentRepository.GetAll();
        //    return View(vm);
        //}

        //[HttpPost]
        //public IActionResult Create(CommentCreateViewModel vm)
        //{
        //    try
        //    {
        //        vm.Comment.CreateDateTime = DateAndTime.Now;
        //        vm.Comment.IsApproved = true;
        //        vm.Comment.UserProfileId = GetCurrentUserProfileId();

        //        _commentRepository.Add(vm.Comment);

        //        return RedirectToAction("Details", new { id = vm.Comment.Id });
        //    }
        //    catch
        //    {
        //        vm.CategoryOptions = _categoryRepository.GetAll();
        //        return View(vm);
        //    }
        //}

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}