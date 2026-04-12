using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.Shares.Commands.RecordShare
{
    public class RecordShareCommand : IRequest<bool>
    {
        public Guid TrackId { get; set; }
        public byte PlatformId { get; set; }
        public string UserId { get; set; }
    }

    public class RecordShareCommandHandler : IRequestHandler<RecordShareCommand, bool>
    {
        private readonly IShareRepositoryAsync _shareRepository;
        private readonly ITrackRepositoryAsync _trackRepository;

        public RecordShareCommandHandler(IShareRepositoryAsync shareRepository, ITrackRepositoryAsync trackRepository)
        {
            _shareRepository = shareRepository;
            _trackRepository = trackRepository;
        }

        public async Task<bool> Handle(RecordShareCommand request, CancellationToken cancellationToken)
        {
            var track = await _trackRepository.GetByIdAsync(request.TrackId);
            if (track == null)
                throw new CleanArchitecture.Core.Exceptions.ApiException($"Track Not Found.");

            var share = new Share
            {
                TrackId = request.TrackId,
                PlatformId = (SharePlatform)request.PlatformId,
                UserId = request.UserId
            };

            await _shareRepository.AddAsync(share);
            return true;
        }
    }
}
