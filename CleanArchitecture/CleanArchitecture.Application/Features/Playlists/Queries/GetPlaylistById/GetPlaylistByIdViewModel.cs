using System;
using System.Collections.Generic;

namespace CleanArchitecture.Core.Features.Playlists.Queries.GetPlaylistById
{
    public class PlaylistTrackViewModel
    {
        public Guid TrackId { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public string CoverImageUrl { get; set; }
        public string FileUrl { get; set; }
    }

    public class GetPlaylistByIdViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        
        public List<PlaylistTrackViewModel> Tracks { get; set; }
        
        public GetPlaylistByIdViewModel()
        {
            Tracks = new List<PlaylistTrackViewModel>();
        }
    }
}
