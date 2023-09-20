using Post.Query.Domain.Entities;

namespace Post.Query.Domain.Repositories
{
    public interface IPostRepository
    {
        Task CreateAsync(PostEntity post);
        Task UpdateAsync(PostEntity post);
        Task DeleteAsync(Guid postId);
        Task<PostEntity> GetByIdAsync(Guid postId);
        Task<List<PostEntity>> GetAllAsync();
        Task<List<PostEntity>> ListByAuthorAsync(string author);
        Task<List<PostEntity>> ListWithLikesAsync(int numerOfLikes);
        Task<List<PostEntity>> ListWithCommantsAsync();
    }
}