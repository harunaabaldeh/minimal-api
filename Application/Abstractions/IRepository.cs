using Domain.Models;

namespace Application.Abstractions
{
    public interface IRepository
    {
        Task<ICollection<Post>> GetPostsWithComments();
        Task<Post> GetPostWithComment(int postId);
        Task<Post> CreatePost(Post toCreate);
        Task<Post> UpdatePost(string updateContent, int postId);
        Task<Post> DeletePost(int postId);
    }
}
