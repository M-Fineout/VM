using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsFor.Data;

namespace WhatsFor.Models
{
    public class PostRepository : IPostRepository
    {
        public IEnumerable<Post> AllPosts { get; set; }
        public readonly ApplicationDbContext Db;

        public PostRepository(ApplicationDbContext Db)
        {
            this.Db = Db;
        }

        public Post CreatePost(string postUserName, Post newPost)
        {

            //newPost.FoodPic.DatePosted = DateTimeOffset.UtcNow.LocalDateTime;
            //newPost.FoodPic.Id = Db.FoodPics.Max(x => x.Id) + 1;

            Db.Add(newPost);
            return newPost;
        }

        public void DeletePost(int postId)
        {
            var post = GetPost(postId);
            if (post != null)
            {
                Db.Remove(post);
            }
        }

        public Post GetPost(int postId)
        {
            var post = Db.AllPosts.Find(postId);
            return post;
        }

        public Post UpdatePost(Post updatedPost, string userName)
        {

            //Actually updating the Post's respective FoodPic
            //No changes to Post actually occur, so don't see a need for updating Post itself, will keep for now though.

            var updatedFoodPicEntity = Db.FoodPics.Attach(updatedPost.FoodPic);
            updatedFoodPicEntity.State = EntityState.Modified;

            var entity = Db.AllPosts.Attach(updatedPost);
            entity.State = EntityState.Modified;

            return updatedPost;
        }

        public Post UpdatePostScore(Post updatedPost, string userName)
        {

            var entity = Db.AllPosts.Attach(updatedPost);
            entity.State = EntityState.Modified;


            return updatedPost;
        }

        public int IncrementScore(int postId, string postUserName)
        {
            var post = GetPost(postId);
            post.Score += 1;

            //TODO:
            //Add this user to the Post's list of HasLiked usernames.
            //post.HasLiked.Add(postUserName);

            //Update post in the Db
            UpdatePostScore(post, postUserName);
            //Save changes
            Commit();
            return post.Score;
        }

        public int DecrementScore(int postId, string postUserName)
        {
            var post = GetPost(postId);
            post.Score -= 1;

            //TODO:
            //Remove this user from the Post's list of HasLiked usernames.
            //NOTE: This could cause errors if user has not liked it yet..
            //post.HasLiked.Remove(postUserName);

            //Update post in the Db
            UpdatePostScore(post, postUserName);
            //Save changes
            Commit();
            return post.Score;
        }

        public int Commit()
        {
            try
            {
                return Db.SaveChanges();
            }
            catch(Exception Ex)
            {
                //Do Something
                return 0;
            }
        }
    }
}
