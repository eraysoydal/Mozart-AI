using System;

namespace CleanArchitecture.Core.Features.Likes.Queries.GetLikesByTrack
{
    public class GetLikesByTrackViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public DateTime LikedAt { get; set; }
    }
}
