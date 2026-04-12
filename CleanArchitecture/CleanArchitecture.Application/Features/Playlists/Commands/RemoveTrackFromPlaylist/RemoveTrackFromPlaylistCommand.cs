using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.Playlists.Commands.RemoveTrackFromPlaylist
{
    public class RemoveTrackFromPlaylistCommand : IRequest<bool>
    {
        public Guid PlaylistId { get; set; }
        public Guid TrackId { get; set; }
        public string UserId { get; set; }
    }

    public class RemoveTrackFromPlaylistCommandHandler : IRequestHandler<RemoveTrackFromPlaylistCommand, bool>
    {
        private readonly IPlaylistRepositoryAsync _playlistRepository;

        public RemoveTrackFromPlaylistCommandHandler(IPlaylistRepositoryAsync playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public async Task<bool> Handle(RemoveTrackFromPlaylistCommand request, CancellationToken cancellationToken)
        {
            var playlist = await _playlistRepository.GetByIdAsync(request.PlaylistId);
            if (playlist == null)
                throw new ApiException($"Playlist Not Found.");

            if (playlist.UserId != request.UserId)
                throw new ApiException($"You are not authorized to modify this playlist.");

            await _playlistRepository.RemoveTrackFromPlaylistAsync(request.PlaylistId, request.TrackId);
            return true;
        }
    }
}
