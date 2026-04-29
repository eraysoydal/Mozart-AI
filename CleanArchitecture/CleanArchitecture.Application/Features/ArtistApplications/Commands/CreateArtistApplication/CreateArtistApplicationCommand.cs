using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;

namespace CleanArchitecture.Core.Features.ArtistApplications.Commands.CreateArtistApplication
{
    public class CreateArtistApplicationCommand : IRequest<Response<Guid>>
    {
        public string PortfolioLinks { get; set; }
        public string IdProofUrl { get; set; }
    }

    public class CreateArtistApplicationCommandHandler : IRequestHandler<CreateArtistApplicationCommand, Response<Guid>>
    {
        private readonly IArtistApplicationRepositoryAsync _artistApplicationRepository;
        private readonly CleanArchitecture.Core.Interfaces.IAuthenticatedUserService _authenticatedUser;

        public CreateArtistApplicationCommandHandler(
            IArtistApplicationRepositoryAsync artistApplicationRepository,
            CleanArchitecture.Core.Interfaces.IAuthenticatedUserService authenticatedUser)
        {
            _artistApplicationRepository = artistApplicationRepository;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Response<Guid>> Handle(CreateArtistApplicationCommand request, CancellationToken cancellationToken)
        {
            var userId = _authenticatedUser.UserId;

            var alreadyPending = await _artistApplicationRepository.HasPendingApplicationAsync(userId);
            if (alreadyPending)
                throw new Exception("You already have a pending artist application.");

            var application = new ArtistApplication
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                IdProofUrl = request.IdProofUrl,
                PortfolioLinks = request.PortfolioLinks,
                StatusId = ApplicationStatus.Pending
            };

            await _artistApplicationRepository.AddAsync(application);

            return new Response<Guid>(application.Id, "Artist application submitted successfully.");
        }
    }
}
