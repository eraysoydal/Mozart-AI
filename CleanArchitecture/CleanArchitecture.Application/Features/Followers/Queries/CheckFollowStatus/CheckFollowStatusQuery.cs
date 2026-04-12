using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.Followers.Queries.CheckFollowStatus
{
    public class CheckFollowStatusQuery : IRequest<bool>
    {
        public string FollowerId { get; set; }
        public string ArtistId { get; set; }
    }

    public class CheckFollowStatusQueryHandler : IRequestHandler<CheckFollowStatusQuery, bool>
    {
        private readonly IFollowerRepositoryAsync _followerRepository;

        public CheckFollowStatusQueryHandler(IFollowerRepositoryAsync followerRepository)
        {
            _followerRepository = followerRepository;
        }

        public async Task<bool> Handle(CheckFollowStatusQuery request, CancellationToken cancellationToken)
        {
            return await _followerRepository.ExistsAsync(request.FollowerId, request.ArtistId);
        }
    }
}
