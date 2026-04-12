using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;

namespace CleanArchitecture.Core.Features.Likes.Queries.GetLikesByUser
{
    public class GetLikesByUserQuery : IRequest<PagedResponse<GetLikesByUserViewModel>>
    {
        public string UserId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetLikesByUserQueryHandler : IRequestHandler<GetLikesByUserQuery, PagedResponse<GetLikesByUserViewModel>>
    {
        private readonly ILikeRepositoryAsync _likeRepository;

        public GetLikesByUserQueryHandler(ILikeRepositoryAsync likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<PagedResponse<GetLikesByUserViewModel>> Handle(GetLikesByUserQuery request, CancellationToken cancellationToken)
        {
            var validFilter = new GetLikesByUserParameter { PageNumber = request.PageNumber, PageSize = request.PageSize };
            var likes = await _likeRepository.GetByUserAsync(request.UserId, validFilter.PageNumber, validFilter.PageSize);
            
            var viewModels = new List<GetLikesByUserViewModel>();
            foreach (var like in likes)
            {
                viewModels.Add(new GetLikesByUserViewModel
                {
                    TrackId = like.TrackId,
                    TrackTitle = like.Track?.Title,
                    ArtistName = like.Track?.Artist?.Username, // Artist's username
                    LikedAt = like.LikedAt
                });
            }

            return new PagedResponse<GetLikesByUserViewModel>(viewModels, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
