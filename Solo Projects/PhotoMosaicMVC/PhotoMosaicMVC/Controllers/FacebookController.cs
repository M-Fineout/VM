using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoMosaicMVC.Controllers
{
    public class FacebookController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public FacebookController(UserManager<IdentityUser> userManager,
                                  SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        // GET: /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            //TODO: Query Facebook photos
            //GET graph.facebook.com/me/photos?access_token=("AccessToken")
            // facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
            // facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];

            // HttpContext.User.GetType();

            

           // GetTokens(AuthenticationProperties)
            //TODO: Build ViewModel 
            return View();
           
        }

        [Authorize]
        public async Task<IActionResult> MyAction()
        {
            var accessToken = await HttpContext.AuthenticateAsync();
            var Workkk = accessToken.Properties.GetTokens();

            var userResult = await HttpContext.AuthenticateAsync();
            var user = userResult.Principal;
            var authProperties = userResult.Properties;
            var tokens = authProperties.GetTokens();

            //var accessToken = authProperties.GetTokenValue("access_token");

            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
              var userAccessToken = info.AuthenticationTokens.Where(x => x.Name == "access_token");
            // Console.WriteLine(info.AuthenticationTokens); 
            var props = HttpContext.Items["properties"];
            Console.WriteLine(props);
                
            return View("FBIndex");
        }
    }
}
