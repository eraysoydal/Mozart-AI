using System;

namespace CleanArchitecture.Core.Entities
{
    public class AIVisual : BaseEntity<Guid>
    {
        public Guid TrackId { get; set; }
        public Track Track { get; set; }

        public string VideoUrl { get; set; }
        public int ViewCount { get; set; }
    }
}
