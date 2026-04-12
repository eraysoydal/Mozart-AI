using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.Followers.Commands.ToggleFollow
{
    public class ToggleFollowCommand : IRequest<bool>
    {
        public string FollowerId { get; set; }
        public string ArtistId { get; set; }
    }

    public class ToggleFollowCommandHandler : IRequestHandler<ToggleFollowCommand, bool>
    {
        private readonly IFollowerRepositoryAsync _followerRepository;

        public ToggleFollowCommandHandler(IFollowerRepositoryAsync followerRepository)
        {
            _followerRepository = followerRepository;
        }

        public async Task<bool> Handle(ToggleFollowCommand request, CancellationToken cancellationToken)
        {
            // A user cannot follow themselves
            if (request.FollowerId == request.ArtistId)
                return false;

            var existingFollow = await _followerRepository.GetAsync(request.FollowerId, request.ArtistId);

            if (existingFollow != null)
            {
                // Unfollow
                await _followerRepository.DeleteAsync(existingFollow);
                return false;
            }
            else
            {
                // Follow
                var newFollower = new Follower
                {
                    FollowerId = request.FollowerId,
                    ArtistId = request.ArtistId
                };
                await _followerRepository.AddAsync(newFollower);
                return true;
            }
        }
    }
}
