using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.Analytics.Commands.RecordStream
{
    public class RecordStreamCommand : IRequest<bool>
    {
        public Guid TrackId { get; set; }
        public string ListenerId { get; set; }
    }

    public class RecordStreamCommandHandler : IRequestHandler<RecordStreamCommand, bool>
    {
        private readonly ITrackStatisticRepositoryAsync _trackStatisticRepository;

        public RecordStreamCommandHandler(ITrackStatisticRepositoryAsync trackStatisticRepository)
        {
            _trackStatisticRepository = trackStatisticRepository;
        }

        public async Task<bool> Handle(RecordStreamCommand request, CancellationToken cancellationToken)
        {
            var stat = new TrackStatistic
            {
                TrackId = request.TrackId,
                ListenerId = request.ListenerId, // Can be null for anonymous
                StreamCount = 1,
                Timestamp = DateTime.UtcNow
            };

            await _trackStatisticRepository.AddAsync(stat);
            return true;
        }
    }
}
