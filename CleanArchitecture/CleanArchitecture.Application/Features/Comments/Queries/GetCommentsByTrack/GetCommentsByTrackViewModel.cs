using System;
using System.Collections.Generic;

namespace CleanArchitecture.Core.Features.Comments.Queries.GetCommentsByTrack
{
    public class GetCommentsByTrackViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public List<GetCommentsByTrackViewModel> Replies { get; set; }
        
        public GetCommentsByTrackViewModel()
        {
            Replies = new List<GetCommentsByTrackViewModel>();
        }
    }
}
