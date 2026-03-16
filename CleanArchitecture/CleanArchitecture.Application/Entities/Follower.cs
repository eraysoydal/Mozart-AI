using System;

namespace CleanArchitecture.Core.Entities
{
    public class Follower
    {
        public string FollowerId { get; set; }
        public User FollowerUser { get; set; }

        public string ArtistId { get; set; }
        public User Artist { get; set; }

        public DateTime FollowedAt { get; set; }
    }
}
