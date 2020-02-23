using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsFor.Data;
using WhatsFor.Models;

namespace WhatsFor.ViewModels
{
    public class ListViewModel
    {
        private readonly IConfiguration Config;
        public IPostRepository _postRepository { get; set; }
        public string Message { get; set; }
        public IEnumerable<Post> AllPosts { get; set; }

        public SignInManager<IdentityUser> _signInManager { get; set; }
        //Might not need this
        public ApplicationDbContext _dbContext { get; set; }

        public ListViewModel(ApplicationDbContext dbContext, SignInManager<IdentityUser> signInManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
        }
    }
}
