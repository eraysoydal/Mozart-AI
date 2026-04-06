using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Features.Genres.Queries.GetAllGenres;
using CleanArchitecture.Core.Features.Tracks.Queries.GetAllTracks;
using CleanArchitecture.Core.Features.Tracks.Commands.CreateTrack;
using CleanArchitecture.Core.Features.Tracks.Commands.UpdateTrack;
using CleanArchitecture.Core.Features.Tracks.Queries.GetTrackById;
using CleanArchitecture.Core.Features.Users.Queries.GetAllUsers;
using CleanArchitecture.Core.Features.Users.Queries.GetAllArtists;

namespace CleanArchitecture.Core.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<GetAllGenresQuery, GetAllGenresParameter>();
            CreateMap<Genre, GetAllGenresViewModel>().ReverseMap();
            CreateMap<Track, GetAllTracksViewModel>()
                .ForMember(dest => dest.AudioFormat, opt => opt.MapFrom(src => src.AudioFormatId.ToString()))
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre != null ? src.Genre.Name : null))
                .ReverseMap();
            CreateMap<GetAllTracksQuery, GetAllTracksParameter>();
            CreateMap<CreateTrackCommand, Track>();
            CreateMap<UpdateTrackCommand, Track>();
            CreateMap<Track, GetTrackByIdViewModel>()
                .ForMember(dest => dest.AudioFormat, opt => opt.MapFrom(src => src.AudioFormatId.ToString()))
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre != null ? src.Genre.Name : null))
                .ReverseMap();

            CreateMap<User, GetAllUsersViewModel>().ReverseMap();
            CreateMap<GetAllUsersQuery, GetAllUsersParameter>();
            CreateMap<GetAllArtistsQuery, GetAllArtistsParameter>();
        }
    }
}
