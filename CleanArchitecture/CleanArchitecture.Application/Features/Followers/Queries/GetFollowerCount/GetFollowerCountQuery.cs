using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.Followers.Queries.GetFollowerCount
{
    public class GetFollowerCountQuery : IRequest<int>
    {
        public string ArtistId { get; set; }
    }

    public class GetFollowerCountQueryHandler : IRequestHandler<GetFollowerCountQuery, int>
    {
        private readonly IFollowerRepositoryAsync _followerRepository;

        public GetFollowerCountQueryHandler(IFollowerRepositoryAsync followerRepository)
        {
            _followerRepository = followerRepository;
        }

        public async Task<int> Handle(GetFollowerCountQuery request, CancellationToken cancellationToken)
        {
            return await _followerRepository.GetFollowerCountAsync(request.ArtistId);
        }
    }
}
