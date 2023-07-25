using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PostRepositories : IPostRepository
    {
        private readonly SocialDbContext _context;
        public PostRepositories(SocialDbContext context)
        {
            _context = context;

        }
        public async Task<Post> CreatePost(Post toCreate)
        {
            toCreate.DateCreated = DateTime.Now;
            toCreate.LastUpdated = DateTime.Now;
            _context.Posts.Add(toCreate);
            await _context.SaveChangesAsync();

            return toCreate;
        }

        public async Task DeletePost(int postId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null) return;

            _context.Posts.Remove(post);

            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Post>> GetAllPost()
        {
            return await _context.Posts.ToListAsync();
        }

        public Task<Post> GetPostById(int postId)
        {
            return _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        }

        public async Task<Post> UpdatePost(string updateContent, int postId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            post.LastUpdated = DateTime.Now;
            post.Content = updateContent;
            return post;
        }
    }
}