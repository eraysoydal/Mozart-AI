using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.Playlists.Commands.UpdatePlaylist
{
    public class UpdatePlaylistCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public string UserId { get; set; }
    }

    public class UpdatePlaylistCommandHandler : IRequestHandler<UpdatePlaylistCommand, Guid>
    {
        private readonly IPlaylistRepositoryAsync _playlistRepository;

        public UpdatePlaylistCommandHandler(IPlaylistRepositoryAsync playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public async Task<Guid> Handle(UpdatePlaylistCommand request, CancellationToken cancellationToken)
        {
            var playlist = await _playlistRepository.GetByIdAsync(request.Id);
            if (playlist == null)
                throw new ApiException($"Playlist Not Found.");

            if (playlist.UserId != request.UserId)
                throw new ApiException($"You are not authorized to update this playlist.");

            playlist.Name = request.Name;
            playlist.IsPublic = request.IsPublic;

            await _playlistRepository.UpdateAsync(playlist);
            return playlist.Id;
        }
    }
}
