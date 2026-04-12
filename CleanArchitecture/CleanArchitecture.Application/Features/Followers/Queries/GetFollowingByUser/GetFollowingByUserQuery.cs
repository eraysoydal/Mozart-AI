using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Core.Filters;
using MediatR;

namespace CleanArchitecture.Core.Features.Followers.Queries.GetFollowingByUser
{
    public class GetFollowingByUserViewModel
    {
        public string ArtistId { get; set; }
        public string ArtistUsername { get; set; }
        public string ArtistProfilePhotoUrl { get; set; }
        public DateTime FollowedAt { get; set; }
    }

    public class GetFollowingByUserParameter : RequestParameter
    {
    }

    public class GetFollowingByUserQuery : IRequest<PagedResponse<GetFollowingByUserViewModel>>
    {
        public string UserId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetFollowingByUserQueryHandler : IRequestHandler<GetFollowingByUserQuery, PagedResponse<GetFollowingByUserViewModel>>
    {
        private readonly IFollowerRepositoryAsync _followerRepository;

        public GetFollowingByUserQueryHandler(IFollowerRepositoryAsync followerRepository)
        {
            _followerRepository = followerRepository;
        }

        public async Task<PagedResponse<GetFollowingByUserViewModel>> Handle(GetFollowingByUserQuery request, CancellationToken cancellationToken)
        {
            var validFilter = new GetFollowingByUserParameter { PageNumber = request.PageNumber, PageSize = request.PageSize };
            var followings = await _followerRepository.GetFollowingByUserAsync(request.UserId, validFilter.PageNumber, validFilter.PageSize);
            
            var viewModels = followings.Select(f => new GetFollowingByUserViewModel
            {
                ArtistId = f.ArtistId,
                ArtistUsername = f.Artist?.Username,
                ArtistProfilePhotoUrl = f.Artist?.ProfilePhotoUrl,
                FollowedAt = f.FollowedAt
            }).ToList();

            return new PagedResponse<GetFollowingByUserViewModel>(viewModels, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
