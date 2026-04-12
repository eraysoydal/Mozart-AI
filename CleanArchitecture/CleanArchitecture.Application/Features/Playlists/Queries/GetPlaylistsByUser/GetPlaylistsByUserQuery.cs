using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using CleanArchitecture.Core.Filters;
using MediatR;

namespace CleanArchitecture.Core.Features.Playlists.Queries.GetPlaylistsByUser
{
    public class GetPlaylistsByUserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
    }

    public class GetPlaylistsByUserParameter : RequestParameter
    {
    }

    public class GetPlaylistsByUserQuery : IRequest<PagedResponse<GetPlaylistsByUserViewModel>>
    {
        public string UserId { get; set; }
        public string CurrentUserId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetPlaylistsByUserQueryHandler : IRequestHandler<GetPlaylistsByUserQuery, PagedResponse<GetPlaylistsByUserViewModel>>
    {
        private readonly IPlaylistRepositoryAsync _playlistRepository;

        public GetPlaylistsByUserQueryHandler(IPlaylistRepositoryAsync playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public async Task<PagedResponse<GetPlaylistsByUserViewModel>> Handle(GetPlaylistsByUserQuery request, CancellationToken cancellationToken)
        {
            var validFilter = new GetPlaylistsByUserParameter { PageNumber = request.PageNumber, PageSize = request.PageSize };
            var playlists = await _playlistRepository.GetByUserAsync(request.UserId, validFilter.PageNumber, validFilter.PageSize);
            
            // Only return private playlists if caller is the owner
            if (request.UserId != request.CurrentUserId)
            {
                playlists = playlists.Where(p => p.IsPublic).ToList();
            }

            var viewModels = playlists.Select(p => new GetPlaylistsByUserViewModel
            {
                Id = p.Id,
                Name = p.Name,
                IsPublic = p.IsPublic
            }).ToList();

            return new PagedResponse<GetPlaylistsByUserViewModel>(viewModels, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
