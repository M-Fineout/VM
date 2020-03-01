using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsFor.Models
{
    public class MockPostRepository : IPostRepository
    {
        public IEnumerable<Post> AllPosts { get; }
        public Post Post;

        public MockPostRepository()
        {
          //  _signInManager = signInManager;
            // _postRepository = postRepository;
            //populate with mock data for testing
            AllPosts = new List<Post>()
            {
               new Post { Id = 1, PostUserName = "Bill", FoodPic = new FoodPic { Name = "Test1" }, Score = 3 },
               new Post { Id = 2, PostUserName = "Ted", FoodPic = new FoodPic { Name = "Test2" }, Score = 5 },
               new Post { Id = 3, PostUserName = "Sammy", FoodPic = new FoodPic { Name = "Test3" }, Score = 7 },
               new Post { Id = 4, PostUserName = "Bob", FoodPic = new FoodPic { Name = "Test4" }, Score = -1 }
            };
        }
        public Post CreatePost(string postUserName, Post newPost)
        {
           // AllPosts.Add(newPost);
            newPost.PostUserName = postUserName;
            newPost.Id = AllPosts.Max(p => p.Id) + 1;
            return newPost;
        }

        public void DeletePost(int postId)
        {
            var post = GetPost(postId);
            if(post != null)
            {
             //   AllPosts.Remove(post); 
            }

        }

        public Post UpdatePost(Post updatedPost, string postUserName)
        {
            var post = GetPost(updatedPost.Id);

            //if(post.PostUserName != postUserName)
            //{
            //    return null;
            //}
            //else
            //{
                post.FoodPic.Name = updatedPost.FoodPic.Name;
                post.FoodPic.Location = updatedPost.FoodPic.Location;
                post.FoodPic.ImgUrl = updatedPost.FoodPic.ImgUrl;
                post.FoodPic.Description = updatedPost.FoodPic.Description;
              //  post.FoodPic.DatePosted = updatedPost.FoodPic.DatePosted;
            //}
            return post;
        }

        public Post GetPost(int postId)
        {
            var post = AllPosts.FirstOrDefault(x => x.Id == postId);
            return post;
        }

        public Post IncrementScore(Post updatedPost, string postUserName)
        {
            
            var post = GetPost(updatedPost.Id);
            updatedPost.Score += 1;
            post.Score = updatedPost.Score;
            return post;
        }

        public int DecrementScore(int postId)
        {
            var post = GetPost(postId);
            post.Score += 1;
            return post.Score;
        }

        public int Commit()
        {
            throw new NotImplementedException();
        }

        public void IncrementScore(int postId, string postUserName)
        {
            throw new NotImplementedException();
        }

        int IPostRepository.IncrementScore(int postId, string postUserName)
        {
            throw new NotImplementedException();
        }

        public void IncrementScoreVoid(int postId, string postUserName)
        {
            throw new NotImplementedException();
        }

        public int DecrementScore(int postId, string postUserName)
        {
            throw new NotImplementedException();
        }
    }
}
