using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Interfaces;
using MediatR;

namespace CleanArchitecture.Core.Features.Playlists.Queries.GetPlaylistById
{
    public class GetPlaylistByIdQuery : IRequest<GetPlaylistByIdViewModel>
    {
        public Guid Id { get; set; }
        public string CurrentUserId { get; set; }
    }

    public class GetPlaylistByIdQueryHandler : IRequestHandler<GetPlaylistByIdQuery, GetPlaylistByIdViewModel>
    {
        private readonly IPlaylistRepositoryAsync _playlistRepository;
        private readonly ICloudFrontService _cloudFrontService;

        public GetPlaylistByIdQueryHandler(IPlaylistRepositoryAsync playlistRepository, ICloudFrontService cloudFrontService)
        {
            _playlistRepository = playlistRepository;
            _cloudFrontService = cloudFrontService;
        }

        public async Task<GetPlaylistByIdViewModel> Handle(GetPlaylistByIdQuery request, CancellationToken cancellationToken)
        {
            var playlist = await _playlistRepository.GetByIdWithTracksAsync(request.Id);
            if (playlist == null) throw new ApiException($"Playlist Not Found.");

            // Check if private
            if (!playlist.IsPublic && playlist.UserId != request.CurrentUserId)
            {
                throw new ApiException($"You are not authorized to view this playlist.");
            }

            var vm = new GetPlaylistByIdViewModel
            {
                Id = playlist.Id,
                Name = playlist.Name,
                IsPublic = playlist.IsPublic,
                UserId = playlist.UserId,
                Username = playlist.User?.Username,
                Tracks = playlist.PlaylistTracks.Select(pt => new PlaylistTrackViewModel
                {
                    TrackId = pt.TrackId,
                    Title = pt.Track?.Title,
                    ArtistName = pt.Track?.Artist?.Username,
                    // Note: CoverImageUrl might not exist yet before Phase 6, but compiling is fine as long as we don't map it here or add it later.
                    // CoverImageUrl = pt.Track?.CoverImageUrl,
                    FileUrl = !string.IsNullOrEmpty(pt.Track?.FileUrl) ? _cloudFrontService.GetSignedUrl(pt.Track.FileUrl) : null
                }).ToList()
            };

            return vm;
        }
    }
}
