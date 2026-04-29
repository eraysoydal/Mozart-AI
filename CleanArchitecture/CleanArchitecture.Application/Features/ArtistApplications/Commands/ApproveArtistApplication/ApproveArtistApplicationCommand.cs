using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;

namespace CleanArchitecture.Core.Features.ArtistApplications.Commands.ApproveArtistApplication
{
    public class ApproveArtistApplicationCommand : IRequest<Response<string>>
    {
        public Guid ApplicationId { get; set; }
        /// <summary>true = Approve, false = Reject</summary>
        public bool IsApproved { get; set; }
    }

    public class ApproveArtistApplicationCommandHandler : IRequestHandler<ApproveArtistApplicationCommand, Response<string>>
    {
        private readonly IArtistApplicationRepositoryAsync _artistApplicationRepository;
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApproveArtistApplicationCommandHandler(
            IArtistApplicationRepositoryAsync artistApplicationRepository,
            IUserRepositoryAsync userRepository,
            IAuthenticatedUserService authenticatedUser)
        {
            _artistApplicationRepository = artistApplicationRepository;
            _userRepository = userRepository;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Response<string>> Handle(ApproveArtistApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = await _artistApplicationRepository.GetByIdGuidAsync(request.ApplicationId);
            if (application == null)
                throw new Exception($"Artist application {request.ApplicationId} not found.");

            if (application.StatusId != ApplicationStatus.Pending)
                throw new Exception("This application has already been reviewed.");

            application.StatusId = request.IsApproved ? ApplicationStatus.Approved : ApplicationStatus.Rejected;
            application.ReviewedBy = _authenticatedUser.UserId;

            await _artistApplicationRepository.UpdateAsync(application);

            if (request.IsApproved)
            {
                // Promote the user's domain role to Artist
                var user = await _userRepository.GetByIdStringAsync(application.UserId);
                if (user != null)
                {
                    user.RoleId = UserRole.Artist;
                    await _userRepository.UpdateAsync(user);
                }
            }

            var msg = request.IsApproved
                ? "Application approved. User has been promoted to Artist."
                : "Application rejected.";

            return new Response<string>(msg);
        }
    }
}
