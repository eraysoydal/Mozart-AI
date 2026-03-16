using System;

namespace CleanArchitecture.Core.Entities
{
    public class TrackStatistic : BaseEntity<Guid>
    {
        public Guid TrackId { get; set; }
        public Track Track { get; set; }

        public int StreamCount { get; set; }
        public DateTime Timestamp { get; set; }

        public string ListenerId { get; set; }
        public User Listener { get; set; }
    }
}
