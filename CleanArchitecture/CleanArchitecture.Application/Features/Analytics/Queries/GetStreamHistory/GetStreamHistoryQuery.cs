using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.Analytics.Queries.GetStreamHistory
{
    public class StreamHistoryDataPoint
    {
        public DateTime Date { get; set; }
        public int Streams { get; set; }
    }

    public class GetStreamHistoryViewModel
    {
        public List<StreamHistoryDataPoint> Data { get; set; }
        
        public GetStreamHistoryViewModel()
        {
            Data = new List<StreamHistoryDataPoint>();
        }
    }

    public class GetStreamHistoryQuery : IRequest<GetStreamHistoryViewModel>
    {
        public string ArtistId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

    public class GetStreamHistoryQueryHandler : IRequestHandler<GetStreamHistoryQuery, GetStreamHistoryViewModel>
    {
        private readonly ITrackStatisticRepositoryAsync _trackStatisticRepository;

        public GetStreamHistoryQueryHandler(ITrackStatisticRepositoryAsync trackStatisticRepository)
        {
            _trackStatisticRepository = trackStatisticRepository;
        }

        public async Task<GetStreamHistoryViewModel> Handle(GetStreamHistoryQuery request, CancellationToken cancellationToken)
        {
            var history = await _trackStatisticRepository.GetStreamHistoryAsync(request.ArtistId, request.From, request.To);

            // Group by Date (ignoring time)
            var groupedData = history
                .GroupBy(h => h.Timestamp.Date)
                .Select(g => new StreamHistoryDataPoint
                {
                    Date = g.Key,
                    Streams = g.Sum(h => h.StreamCount)
                })
                .OrderBy(d => d.Date)
                .ToList();

            return new GetStreamHistoryViewModel
            {
                Data = groupedData
            };
        }
    }
}
