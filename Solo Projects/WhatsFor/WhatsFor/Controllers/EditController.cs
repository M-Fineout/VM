using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using WhatsFor.Data;
using WhatsFor.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatsFor.Controllers
{
    public class EditController : Controller
    {
        public SignInManager<IdentityUser> _signInManager;
        private IHtmlHelper htmlHelper;
        public IPostRepository PostRepository;
        private readonly ApplicationDbContext _dbContext;
        //public IEnumerable<SelectListItem> AllFoodTags { get; set; }
        [BindProperty]
        public Post Post { get; set; }
        public EditController(IPostRepository PostRepository,
                              IHtmlHelper htmlHelper,
                              SignInManager<IdentityUser> signInManager,
                              ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            this.htmlHelper = htmlHelper;
            this.PostRepository = PostRepository;
        }
        public IActionResult Edit()
        {
            return View();
        }


        [HttpGet]
        public IActionResult EditPost(int? postId)
        {
            if (postId.HasValue)
            {
                Post = _dbContext.AllPosts.Include(post => post.FoodPic)
                                         .FirstOrDefault(x => x.Id == postId);

            }
            else
            {
                Post = new Post();
            }
            if (Post == null)
            {
                return RedirectToPage("./NotFound");
            }
            return View(Post);
        }
        

        [HttpPost]
        public void IncreaseScoreAction(int postId)
        {
            PostRepository.IncrementScore(postId, this.User.Identity.Name);
            PostRepository.Commit();

            //Way to retrieve userId until further notice
            //var temp = _dbContext.Users.FirstOrDefault(x => x.UserName == this.User.Identity.Name);
            //var userId = temp.Id; 
        }

        [HttpPost]
        public void DecreaseScoreAction(int postId)
        {
            PostRepository.DecrementScore(postId, this.User.Identity.Name);
            PostRepository.Commit();

        }

    }

   
}
