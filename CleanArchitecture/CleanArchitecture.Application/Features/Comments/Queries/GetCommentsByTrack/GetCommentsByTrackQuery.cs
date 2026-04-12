using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;

namespace CleanArchitecture.Core.Features.Comments.Queries.GetCommentsByTrack
{
    public class GetCommentsByTrackQuery : IRequest<PagedResponse<GetCommentsByTrackViewModel>>
    {
        public Guid TrackId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetCommentsByTrackQueryHandler : IRequestHandler<GetCommentsByTrackQuery, PagedResponse<GetCommentsByTrackViewModel>>
    {
        private readonly ICommentRepositoryAsync _commentRepository;

        public GetCommentsByTrackQueryHandler(ICommentRepositoryAsync commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<PagedResponse<GetCommentsByTrackViewModel>> Handle(GetCommentsByTrackQuery request, CancellationToken cancellationToken)
        {
            var validFilter = new GetCommentsByTrackParameter { PageNumber = request.PageNumber, PageSize = request.PageSize };
            var comments = await _commentRepository.GetByTrackAsync(request.TrackId, validFilter.PageNumber, validFilter.PageSize);
            
            var viewModels = comments.Select(MapCommentToViewModel).ToList();

            return new PagedResponse<GetCommentsByTrackViewModel>(viewModels, validFilter.PageNumber, validFilter.PageSize);
        }

        private GetCommentsByTrackViewModel MapCommentToViewModel(Comment comment)
        {
            var vm = new GetCommentsByTrackViewModel
            {
                Id = comment.Id,
                UserId = comment.UserId,
                Username = comment.User?.Username,
                ProfilePhotoUrl = comment.User?.ProfilePhotoUrl,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt
            };

            if (comment.Replies != null && comment.Replies.Any())
            {
                vm.Replies = comment.Replies.Select(MapCommentToViewModel).OrderBy(r => r.CreatedAt).ToList();
            }

            return vm;
        }
    }
}
