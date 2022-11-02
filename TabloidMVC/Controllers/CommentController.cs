using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public IActionResult Index(int id) //id is the postId
        {
            //get comments and sort by date - most recent first
            List<Comment> comments = _commentRepository.GetPostsComments(id).OrderByDescending(x => x.CreateDateTime).ToList();
            return View(comments);
        }

        //details method commented out for now

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

        public IActionResult Create(int id) //id is the postId
        {            
            return View();
        }

        [HttpPost]
        public IActionResult Create(int id, Comment comment) //id is the postId
        {
            try
            {
                comment.CreateDateTime = DateTime.Now;
                comment.PostId = id;
                comment.UserProfileId = GetCurrentUserProfileId();
                //comment.UserDisplayName = _userRepository.GetUserById(comment.UserProfileId).DisplayName;
                _commentRepository.Add(comment);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(comment);
            }
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
