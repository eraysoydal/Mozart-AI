using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.Playlists.Commands.DeletePlaylist
{
    public class DeletePlaylistCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
    }

    public class DeletePlaylistCommandHandler : IRequestHandler<DeletePlaylistCommand, Guid>
    {
        private readonly IPlaylistRepositoryAsync _playlistRepository;

        public DeletePlaylistCommandHandler(IPlaylistRepositoryAsync playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public async Task<Guid> Handle(DeletePlaylistCommand request, CancellationToken cancellationToken)
        {
            var playlist = await _playlistRepository.GetByIdAsync(request.Id);
            if (playlist == null)
                throw new ApiException($"Playlist Not Found.");

            if (playlist.UserId != request.UserId)
                throw new ApiException($"You are not authorized to delete this playlist.");

            await _playlistRepository.DeleteAsync(playlist);
            return playlist.Id;
        }
    }
}
