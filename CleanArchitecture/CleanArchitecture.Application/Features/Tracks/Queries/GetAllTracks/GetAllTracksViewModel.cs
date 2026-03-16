using System;

namespace CleanArchitecture.Core.Features.Tracks.Queries.GetAllTracks
{
    public class GetAllTracksViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ArtistId { get; set; }
        public int DurationSeconds { get; set; }
        public bool IsAiGenerated { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string AudioFormat { get; set; }
        public string FileUrl { get; set; }
        public string CanvasUrl { get; set; }
    }
}
