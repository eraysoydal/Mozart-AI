using System;

namespace CleanArchitecture.Core.Features.Tracks.Queries.GetTrackById
{
    public class GetTrackByIdViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ArtistId { get; set; }
        public int? GenreId { get; set; }
        public string GenreName { get; set; }
        public bool IsAiGenerated { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string AudioFormat { get; set; }
        public string FileUrl { get; set; }
        public string CanvasUrl { get; set; }
        public string CoverImageUrl { get; set; }
        public string LyricSyncUrl { get; set; }
        public string Lyrics { get; set; }
        public string PLine { get; set; }
        public int AiPermission { get; set; }
        public bool AllowSystemAnalysis { get; set; }
    }
}
