using System;

namespace CleanArchitecture.Core.Features.Likes.Queries.GetLikesByUser
{
    public class GetLikesByUserViewModel
    {
        public Guid TrackId { get; set; }
        public string TrackTitle { get; set; }
        public string ArtistName { get; set; }
        public string CoverImageUrl { get; set; }
        public DateTime LikedAt { get; set; }
    }
}
