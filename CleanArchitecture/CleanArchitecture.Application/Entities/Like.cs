using System;

namespace CleanArchitecture.Core.Entities
{
    public class Like
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public Guid TrackId { get; set; }
        public Track Track { get; set; }

        public DateTime LikedAt { get; set; }
    }
}
