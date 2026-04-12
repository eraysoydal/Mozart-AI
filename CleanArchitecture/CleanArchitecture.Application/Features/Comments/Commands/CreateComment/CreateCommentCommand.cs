using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.Comments.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<Guid>
    {
        public Guid TrackId { get; set; }
        public string Content { get; set; }
        public Guid? ParentId { get; set; }
        public string UserId { get; set; }
    }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
    {
        private readonly ICommentRepositoryAsync _commentRepository;
        private readonly ITrackRepositoryAsync _trackRepository;

        public CreateCommentCommandHandler(ICommentRepositoryAsync commentRepository, ITrackRepositoryAsync trackRepository)
        {
            _commentRepository = commentRepository;
            _trackRepository = trackRepository;
        }

        public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var track = await _trackRepository.GetByIdAsync(request.TrackId);
            if (track == null)
                throw new CleanArchitecture.Core.Exceptions.ApiException($"Track Not Found.");

            var comment = new Comment
            {
                TrackId = request.TrackId,
                Content = request.Content,
                ParentId = request.ParentId,
                UserId = request.UserId
            };

            await _commentRepository.AddAsync(comment);
            return comment.Id;
        }
    }
}
