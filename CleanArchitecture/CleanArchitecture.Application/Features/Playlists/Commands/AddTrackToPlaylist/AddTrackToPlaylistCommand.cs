using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.Playlists.Commands.AddTrackToPlaylist
{
    public class AddTrackToPlaylistCommand : IRequest<bool>
    {
        public Guid PlaylistId { get; set; }
        public Guid TrackId { get; set; }
        public string UserId { get; set; }
    }

    public class AddTrackToPlaylistCommandHandler : IRequestHandler<AddTrackToPlaylistCommand, bool>
    {
        private readonly IPlaylistRepositoryAsync _playlistRepository;

        public AddTrackToPlaylistCommandHandler(IPlaylistRepositoryAsync playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public async Task<bool> Handle(AddTrackToPlaylistCommand request, CancellationToken cancellationToken)
        {
            var playlist = await _playlistRepository.GetByIdAsync(request.PlaylistId);
            if (playlist == null)
                throw new ApiException($"Playlist Not Found.");

            if (playlist.UserId != request.UserId)
                throw new ApiException($"You are not authorized to modify this playlist.");

            var hasTrack = await _playlistRepository.HasTrackAsync(request.PlaylistId, request.TrackId);
            if (!hasTrack)
            {
                await _playlistRepository.AddTrackToPlaylistAsync(request.PlaylistId, request.TrackId);
            }

            return true;
        }
    }
}
