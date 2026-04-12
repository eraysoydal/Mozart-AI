using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.Likes.Queries.CheckLikeStatus
{
    public class CheckLikeStatusQuery : IRequest<bool>
    {
        public Guid TrackId { get; set; }
        public string UserId { get; set; }
    }

    public class CheckLikeStatusQueryHandler : IRequestHandler<CheckLikeStatusQuery, bool>
    {
        private readonly ILikeRepositoryAsync _likeRepository;

        public CheckLikeStatusQueryHandler(ILikeRepositoryAsync likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<bool> Handle(CheckLikeStatusQuery request, CancellationToken cancellationToken)
        {
            return await _likeRepository.ExistsAsync(request.UserId, request.TrackId);
        }
    }
}
