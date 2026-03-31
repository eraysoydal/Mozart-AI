using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Features.Genres.Queries.GetAllGenres;
using CleanArchitecture.Core.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Core.Features.Products.Queries.GetAllProducts;
using CleanArchitecture.Core.Features.Tracks.Queries.GetAllTracks;
using CleanArchitecture.Core.Features.Tracks.Commands.CreateTrack;
using CleanArchitecture.Core.Features.Tracks.Commands.UpdateTrack;
using CleanArchitecture.Core.Features.Tracks.Queries.GetTrackById;

namespace CleanArchitecture.Core.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
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
        }
    }
}
