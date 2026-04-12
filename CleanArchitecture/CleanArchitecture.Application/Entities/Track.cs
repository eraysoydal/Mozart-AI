using System;
using System.Collections.Generic;
using CleanArchitecture.Core.Enums;

namespace CleanArchitecture.Core.Entities
{
    public class Track : BaseEntity<Guid>
    {
        public string Title { get; set; }
        
        public string ArtistId { get; set; }
        public User Artist { get; set; }

        public string FileUrl { get; set; }
        public int? GenreId { get; set; }
        public Genre Genre { get; set; }
        public bool IsAiGenerated { get; set; }

        public Guid? RefTrackId { get; set; }
        public Track RefTrack { get; set; }

        public DateTime ReleaseDate { get; set; }
        public string CanvasUrl { get; set; }
        public string CoverImageUrl { get; set; }
        public string LyricSyncUrl { get; set; }
        public string Lyrics { get; set; }
        
        public AudioFormat AudioFormatId { get; set; }
        
        public string PLine { get; set; }
        public int AiPermission { get; set; }
        public bool AllowSystemAnalysis { get; set; }

        public ICollection<Track> DerivedTracks { get; set; }
        public ICollection<AIVisual> AIVisuals { get; set; }
        public ICollection<GenerativePrompt> GenerativePrompts { get; set; }
        public ICollection<TrackStatistic> TrackStatistics { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PlaylistTrack> PlaylistTracks { get; set; }
    }
}
