using CleanArchitecture.Core.Filters;

namespace CleanArchitecture.Core.Features.Tracks.Queries.GetAllTracks
{
    public class GetAllTracksParameter : RequestParameter
    {
        public string SearchQuery { get; set; }
    }
}
