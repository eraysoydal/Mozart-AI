using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Tracks.Commands.UpdateTrack
{
    public class UpdateTrackCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int? GenreId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public byte AudioFormatId { get; set; }
        public int AiPermission { get; set; }
        public bool AllowSystemAnalysis { get; set; }
        public string CanvasUrl { get; set; }
        public string CoverImageUrl { get; set; }
        public string Lyrics { get; set; }
        public bool IsAiGenerated { get; set; }
        
        public class UpdateTrackCommandHandler : IRequestHandler<UpdateTrackCommand, Guid>
        {
            private readonly ITrackRepositoryAsync _trackRepository;
            private readonly IAuthenticatedUserService _authenticatedUserService;

            public UpdateTrackCommandHandler(ITrackRepositoryAsync trackRepository, IAuthenticatedUserService authenticatedUserService)
            {
                _trackRepository = trackRepository;
                _authenticatedUserService = authenticatedUserService;
            }

            public async Task<Guid> Handle(UpdateTrackCommand command, CancellationToken cancellationToken)
            {
                var track = await _trackRepository.GetByIdAsync(command.Id);
                if (track == null)
                    throw new EntityNotFoundException("Track", command.Id);

                if (track.ArtistId != _authenticatedUserService.UserId)
                    throw new ApiException("You are not authorized to update this track.");

                track.Title = command.Title;
                track.GenreId = command.GenreId;
                track.ReleaseDate = command.ReleaseDate;
                track.AudioFormatId = (Core.Enums.AudioFormat)command.AudioFormatId;
                track.AiPermission = command.AiPermission;
                track.AllowSystemAnalysis = command.AllowSystemAnalysis;
                track.CanvasUrl = command.CanvasUrl;
                track.CoverImageUrl = command.CoverImageUrl;
                track.Lyrics = command.Lyrics;
                track.IsAiGenerated = command.IsAiGenerated;

                await _trackRepository.UpdateAsync(track);
                return track.Id;
            }
        }
    }
}
