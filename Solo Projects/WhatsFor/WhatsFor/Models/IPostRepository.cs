using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsFor.Models
{
    public interface IPostRepository
    {
        Post CreatePost(string postUserName, Post newPost);
        Post GetPost(int postId);
        Post UpdatePost(Post updatedPost, string postUserName);
        void DeletePost(int postId);
        int Commit();
        IEnumerable<Post> AllPosts { get; }
        int IncrementScore(int postId, string postUserName);
        //void IncrementScoreVoid(int postId, string postUserName);
        int DecrementScore(int postId, string postUserName);

    }
}
