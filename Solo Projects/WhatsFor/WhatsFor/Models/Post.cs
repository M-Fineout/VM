using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhatsFor.Models
{
    public class Post
    {
        public Post()
        {

        }

        public int Id { get; set; } 
        public string PostUserName { get; set; }
        public FoodPic FoodPic { get; set; }
        //Db can't save List<string> -- need to extrapolate this to separate entity
        //public List<string> HasLiked { get; set; }

        [BindProperty]
        public int Score { get; set; } = 0;

        public int IncrementScore(int score)
        {
            score += 1;
            Score = score;
            return score;
        }

        public int DecrementScore(int score)
        {
            score -= 1;
            Score = score;
            return score;
        }
    }
}