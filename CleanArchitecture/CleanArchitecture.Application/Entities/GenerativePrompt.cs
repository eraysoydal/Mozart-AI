using System;

namespace CleanArchitecture.Core.Entities
{
    public class GenerativePrompt : BaseEntity<Guid>
    {
        public Guid TrackId { get; set; }
        public Track Track { get; set; }

        public int? GenreId { get; set; }
        public Genre Genre { get; set; }
        public string MoodTag { get; set; }
        public string InstrumentTags { get; set; }
        public string RawPromptText { get; set; }
    }
}
