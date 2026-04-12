using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Core.Filters;
using MediatR;

namespace CleanArchitecture.Core.Features.Followers.Queries.GetFollowersByArtist
{
    public class GetFollowersByArtistViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public DateTime FollowedAt { get; set; }
    }

    public class GetFollowersByArtistParameter : RequestParameter
    {
    }

    public class GetFollowersByArtistQuery : IRequest<PagedResponse<GetFollowersByArtistViewModel>>
    {
        public string ArtistId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetFollowersByArtistQueryHandler : IRequestHandler<GetFollowersByArtistQuery, PagedResponse<GetFollowersByArtistViewModel>>
    {
        private readonly IFollowerRepositoryAsync _followerRepository;

        public GetFollowersByArtistQueryHandler(IFollowerRepositoryAsync followerRepository)
        {
            _followerRepository = followerRepository;
        }

        public async Task<PagedResponse<GetFollowersByArtistViewModel>> Handle(GetFollowersByArtistQuery request, CancellationToken cancellationToken)
        {
            var validFilter = new GetFollowersByArtistParameter { PageNumber = request.PageNumber, PageSize = request.PageSize };
            var followers = await _followerRepository.GetFollowersByArtistAsync(request.ArtistId, validFilter.PageNumber, validFilter.PageSize);
            
            var viewModels = followers.Select(f => new GetFollowersByArtistViewModel
            {
                UserId = f.FollowerId,
                Username = f.FollowerUser?.Username,
                ProfilePhotoUrl = f.FollowerUser?.ProfilePhotoUrl,
                FollowedAt = f.FollowedAt
            }).ToList();

            return new PagedResponse<GetFollowersByArtistViewModel>(viewModels, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
