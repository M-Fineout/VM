using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WhatsFor.Data;
using WhatsFor.Models;
using WhatsFor.ViewModels;

namespace WhatsFor.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration Config;
        public IPostRepository PostRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, 
                              IPostRepository PostRepository, 
                              IConfiguration Config,
                              ApplicationDbContext dbContext,
                              SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            this.PostRepository = PostRepository;
            this.Config = Config;
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ViewResult List()
        {
            // Is there a better way to get all of these posts to memory
            // Possibly show only 10 posts per page .Take(10) to cut down on memory..
                var allPosts = _dbContext.AllPosts
                                      .Include(post => post.FoodPic)
                                      .ToList();

            ListViewModel model = new ListViewModel(_dbContext, _signInManager)
            {
                AllPosts = allPosts,
                _postRepository = PostRepository
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult SubmitPost(Post post)
        {
             
            if (post.Id > 0)
            {
                PostRepository.UpdatePost(post, this.User.Identity.Name);
                TempData["Message"] = "Post Updated";
            }
            else
            {
                PostRepository.CreatePost(this.User.Identity.Name, post);
                TempData["Message"] = "Post Added";
            }
            PostRepository.Commit();

            // This will keep user from resubmitting form via the refresh button
            return RedirectToAction("List"); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
