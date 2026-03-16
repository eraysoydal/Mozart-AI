using System;
using System.Collections.Generic;

namespace CleanArchitecture.Core.Entities
{
    public class Playlist : BaseEntity<Guid>
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string Name { get; set; }
        public bool IsPublic { get; set; }

        public ICollection<PlaylistTrack> PlaylistTracks { get; set; }
    }
}
