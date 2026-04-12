using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;

namespace CleanArchitecture.Core.Features.Likes.Queries.GetLikesByTrack
{
    public class GetLikesByTrackQuery : IRequest<PagedResponse<GetLikesByTrackViewModel>>
    {
        public Guid TrackId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetLikesByTrackQueryHandler : IRequestHandler<GetLikesByTrackQuery, PagedResponse<GetLikesByTrackViewModel>>
    {
        private readonly ILikeRepositoryAsync _likeRepository;

        public GetLikesByTrackQueryHandler(ILikeRepositoryAsync likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<PagedResponse<GetLikesByTrackViewModel>> Handle(GetLikesByTrackQuery request, CancellationToken cancellationToken)
        {
            var validFilter = new GetLikesByTrackParameter { PageNumber = request.PageNumber, PageSize = request.PageSize };
            var likes = await _likeRepository.GetByTrackAsync(request.TrackId, validFilter.PageNumber, validFilter.PageSize);
            
            var viewModels = new List<GetLikesByTrackViewModel>();
            foreach (var like in likes)
            {
                viewModels.Add(new GetLikesByTrackViewModel
                {
                    UserId = like.UserId,
                    Username = like.User?.Username,
                    ProfilePhotoUrl = like.User?.ProfilePhotoUrl,
                    LikedAt = like.LikedAt
                });
            }

            return new PagedResponse<GetLikesByTrackViewModel>(viewModels, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
