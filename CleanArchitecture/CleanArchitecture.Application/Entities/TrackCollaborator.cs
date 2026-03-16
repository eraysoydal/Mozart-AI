using System;

namespace CleanArchitecture.Core.Entities
{
    public class TrackCollaborator
    {
        public Guid TrackId { get; set; }
        public Track Track { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string Role { get; set; }
    }
}
