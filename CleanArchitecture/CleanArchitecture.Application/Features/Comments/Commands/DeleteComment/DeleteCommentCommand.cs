using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } // To verify ownership
    }

    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Guid>
    {
        private readonly ICommentRepositoryAsync _commentRepository;

        public DeleteCommentCommandHandler(ICommentRepositoryAsync commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Guid> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetByIdAsync(request.Id);
            if (comment == null)
                throw new ApiException("Comment Not Found.");

            // Basic authorization check: Only the author can delete their comment
            if (comment.UserId != request.UserId)
                throw new ApiException("You are not authorized to delete this comment.");

            await _commentRepository.DeleteAsync(comment);
            return comment.Id;
        }
    }
}
