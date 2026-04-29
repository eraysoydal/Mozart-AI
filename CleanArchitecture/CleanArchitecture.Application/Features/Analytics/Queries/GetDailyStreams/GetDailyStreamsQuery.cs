using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.Analytics.Queries.GetDailyStreams
{
    public class DailyStreamDataPoint
    {
        public string Date { get; set; }   // "YYYY-MM-DD"
        public int Streams { get; set; }
        public int UniqueListeners { get; set; }
    }

    public class GetDailyStreamsQuery : IRequest<List<DailyStreamDataPoint>>
    {
        public string ArtistId { get; set; }
        /// <summary>How many past days to include (default 30).</summary>
        public int Days { get; set; } = 30;
    }

    public class GetDailyStreamsQueryHandler : IRequestHandler<GetDailyStreamsQuery, List<DailyStreamDataPoint>>
    {
        private readonly ITrackStatisticRepositoryAsync _trackStatisticRepository;

        public GetDailyStreamsQueryHandler(ITrackStatisticRepositoryAsync trackStatisticRepository)
        {
            _trackStatisticRepository = trackStatisticRepository;
        }

        public async Task<List<DailyStreamDataPoint>> Handle(GetDailyStreamsQuery request, CancellationToken cancellationToken)
        {
            var to = DateTime.UtcNow.Date.AddDays(1).AddTicks(-1);   // end of today
            var from = DateTime.UtcNow.Date.AddDays(-request.Days);

            var history = await _trackStatisticRepository.GetStreamHistoryAsync(request.ArtistId, from, to);

            var grouped = history
                .GroupBy(h => h.Timestamp.Date)
                .Select(g => new DailyStreamDataPoint
                {
                    Date = g.Key.ToString("yyyy-MM-dd"),
                    Streams = g.Sum(h => h.StreamCount),
                    UniqueListeners = g.Select(h => h.ListenerId).Distinct().Count()
                })
                .OrderBy(d => d.Date)
                .ToList();

            return grouped;
        }
    }
}
