using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.Analytics.Queries.GetDashboardStats
{
    public class GetDashboardStatsViewModel
    {
        public int TotalFollowers { get; set; }
        public int MonthlyListeners { get; set; }
        public int TotalTracks { get; set; }
        public int TotalStreams { get; set; }
    }

    public class GetDashboardStatsQuery : IRequest<GetDashboardStatsViewModel>
    {
        public string ArtistId { get; set; }
    }

    public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, GetDashboardStatsViewModel>
    {
        private readonly ITrackStatisticRepositoryAsync _trackStatisticRepository;
        private readonly IFollowerRepositoryAsync _followerRepository;

        public GetDashboardStatsQueryHandler(
            ITrackStatisticRepositoryAsync trackStatisticRepository,
            IFollowerRepositoryAsync followerRepository)
        {
            _trackStatisticRepository = trackStatisticRepository;
            _followerRepository = followerRepository;
        }

        public async Task<GetDashboardStatsViewModel> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            var totalFollowers = await _followerRepository.GetFollowerCountAsync(request.ArtistId);
            var monthlyListeners = await _trackStatisticRepository.GetMonthlyListenersByArtistAsync(request.ArtistId);
            var totalTracks = await _trackStatisticRepository.GetTrackCountByArtistAsync(request.ArtistId);
            var totalStreams = await _trackStatisticRepository.GetTotalStreamsByArtistAsync(request.ArtistId);

            return new GetDashboardStatsViewModel
            {
                TotalFollowers = totalFollowers,
                MonthlyListeners = monthlyListeners,
                TotalTracks = totalTracks,
                TotalStreams = totalStreams
            };
        }
    }
}
