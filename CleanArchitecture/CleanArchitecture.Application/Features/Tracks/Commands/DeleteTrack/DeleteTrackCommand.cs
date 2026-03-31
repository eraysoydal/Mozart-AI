using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Tracks.Commands.DeleteTrack
{
    public class DeleteTrackCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public class DeleteTrackCommandHandler : IRequestHandler<DeleteTrackCommand, Guid>
        {
            private readonly ITrackRepositoryAsync _trackRepository;
            private readonly IAuthenticatedUserService _authenticatedUserService;

            public DeleteTrackCommandHandler(ITrackRepositoryAsync trackRepository, IAuthenticatedUserService authenticatedUserService)
            {
                _trackRepository = trackRepository;
                _authenticatedUserService = authenticatedUserService;
            }

            public async Task<Guid> Handle(DeleteTrackCommand command, CancellationToken cancellationToken)
            {
                var track = await _trackRepository.GetByIdAsync(command.Id);
                if (track == null)
                    throw new EntityNotFoundException("Track", command.Id);

                if (track.ArtistId != _authenticatedUserService.UserId)
                    throw new ApiException("You are not authorized to delete this track.");

                await _trackRepository.DeleteAsync(track);
                return track.Id;
            }
        }
    }
}
