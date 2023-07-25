using Application.Abstractions;
using Application.Posts.QueryHandlers;
using Domain.Models;
using MediatR;

namespace Application.Posts.Queries
{
    public class GetPostByIdHandler : IRequestHandler<GetPostById, Post>
    {
        private readonly IPostRepository _repository;
        public GetPostByIdHandler(IPostRepository repository)
        {
            _repository = repository;

        }
        public async Task<Post> Handle(GetPostById request, CancellationToken cancellationToken)
        {
            return await _repository.GetPostById(request.PostId);
        }
    }
}