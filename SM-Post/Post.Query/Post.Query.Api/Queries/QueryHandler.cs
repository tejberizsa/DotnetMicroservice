using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;

namespace Post.Query.Api.Queries
{
    public class QueryHandler : IQueryHandler
    {
        private readonly IPostRepository _postRepository;

        public QueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public Task<List<PostEntity>> HandleAsync(FindAllPostsQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostEntity>> HandleAsync(FindPostByIdQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostEntity>> HandleAsync(FindPostsByAuthorQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostEntity>> HandleAsync(FindPostsWithCommentQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostEntity>> HandleAsync(FindPostsWithLikesQuery query)
        {
            throw new NotImplementedException();
        }
    }
}