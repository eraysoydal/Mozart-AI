using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.Likes.Commands.ToggleLike
{
    public class ToggleLikeCommand : IRequest<bool>
    {
        public Guid TrackId { get; set; }
        public string UserId { get; set; }
    }

    public class ToggleLikeCommandHandler : IRequestHandler<ToggleLikeCommand, bool>
    {
        private readonly ILikeRepositoryAsync _likeRepository;
        private readonly ITrackRepositoryAsync _trackRepository;

        public ToggleLikeCommandHandler(ILikeRepositoryAsync likeRepository, ITrackRepositoryAsync trackRepository)
        {
            _likeRepository = likeRepository;
            _trackRepository = trackRepository;
        }

        public async Task<bool> Handle(ToggleLikeCommand request, CancellationToken cancellationToken)
        {
            var track = await _trackRepository.GetByIdAsync(request.TrackId);
            if (track == null)
                throw new CleanArchitecture.Core.Exceptions.ApiException($"Track Not Found.");

            var existingLike = await _likeRepository.GetAsync(request.UserId, request.TrackId);

            if (existingLike != null)
            {
                // Unlike
                await _likeRepository.DeleteAsync(existingLike);
                return false; // Result is false (unliked)
            }
            else
            {
                // Like
                var newLike = new Like
                {
                    UserId = request.UserId,
                    TrackId = request.TrackId
                };
                await _likeRepository.AddAsync(newLike);
                return true; // Result is true (liked)
            }
        }
    }
}
