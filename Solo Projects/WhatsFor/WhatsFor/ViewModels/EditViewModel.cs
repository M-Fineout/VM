using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsFor.Models;

namespace WhatsFor.ViewModels
{
    public class EditViewModel
    {
        public IEnumerable<Post> AllPosts { get; set; }
        public FoodPic Foodpic { get; set; }

    }
}
