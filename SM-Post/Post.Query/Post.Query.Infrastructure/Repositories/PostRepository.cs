using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;
using Post.Query.Infrastructure.DataAccess;

namespace Post.Query.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContextFactory _contextFactory;

        public PostRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateAsync(PostEntity post)
        {
            using DatabaseContext context = _contextFactory.CreateContext();
            context.Posts.Add(post);

            _ = await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid postId)
        {
            using DatabaseContext context = _contextFactory.CreateContext();
            var post = await GetByIdAsync(postId);

            if(post == null) return;

            context.Posts.Remove(post);
            _ = await context.SaveChangesAsync();
        }

        public async Task<List<PostEntity>> GetAllAsync()
        {
            using DatabaseContext context = _contextFactory.CreateContext();
            return await context.Posts.AsNoTracking()
                                .Include(p => p.Comments).AsNoTracking()
                                .ToListAsync();
        }

        public async Task<PostEntity> GetByIdAsync(Guid postId)
        {
            using DatabaseContext context = _contextFactory.CreateContext();
            return await context.Posts
                                .Include(p => p.Comments)
                                .FirstOrDefaultAsync(x => x.PostId == postId);
        }

        public async Task<List<PostEntity>> ListByAuthorAsync(string author)
        {
            using DatabaseContext context = _contextFactory.CreateContext();
            return await context.Posts.AsNoTracking()
                                .Include(p => p.Comments).AsNoTracking()
                                .Where(x => x.Author.Contains(author))
                                .ToListAsync();
        }

        public async Task<List<PostEntity>> ListWithCommantsAsync()
        {
            using DatabaseContext context = _contextFactory.CreateContext();
            return await context.Posts.AsNoTracking()
                                .Include(p => p.Comments).AsNoTracking()
                                .Where(x => x.Comments != null && x.Comments.Any())
                                .ToListAsync();
        }

        public async Task<List<PostEntity>> ListWithLikesAsync(int numerOfLikes)
        {
            using DatabaseContext context = _contextFactory.CreateContext();
            return await context.Posts.AsNoTracking()
                                .Include(p => p.Comments).AsNoTracking()
                                .Where(x => x.Likes >= numerOfLikes)
                                .ToListAsync();
        }

        public async Task UpdateAsync(PostEntity post)
        {
            using DatabaseContext context = _contextFactory.CreateContext();
            context.Posts.Update(post);

            _ = await context.SaveChangesAsync();
        }
    }
}
   