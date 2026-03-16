using System;
using CleanArchitecture.Core.Enums;

namespace CleanArchitecture.Core.Entities
{
    public class Share
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public Guid TrackId { get; set; }
        public Track Track { get; set; }

        public SharePlatform PlatformId { get; set; }
        public DateTime SharedAt { get; set; }
    }
}
