using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.Playlists.Commands.CreatePlaylist
{
    public class CreatePlaylistCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public string UserId { get; set; }
    }

    public class CreatePlaylistCommandHandler : IRequestHandler<CreatePlaylistCommand, Guid>
    {
        private readonly IPlaylistRepositoryAsync _playlistRepository;

        public CreatePlaylistCommandHandler(IPlaylistRepositoryAsync playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public async Task<Guid> Handle(CreatePlaylistCommand request, CancellationToken cancellationToken)
        {
            var playlist = new Playlist
            {
                Name = request.Name,
                IsPublic = request.IsPublic,
                UserId = request.UserId
            };

            await _playlistRepository.AddAsync(playlist);
            return playlist.Id;
        }
    }
}
