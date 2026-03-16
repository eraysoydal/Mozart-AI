using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Features.Categories.Queries.GetAllCategories;
using CleanArchitecture.Core.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Core.Features.Products.Queries.GetAllProducts;
using CleanArchitecture.Core.Features.Tracks.Queries.GetAllTracks;
using CleanArchitecture.Core.Features.Tracks.Commands.CreateTrack;

namespace CleanArchitecture.Core.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
            CreateMap<GetAllCategoriesQuery, GetAllCategoriesParameter>();
            CreateMap<Category, GetAllCategoriesViewModel>().ReverseMap();
            CreateMap<Track, GetAllTracksViewModel>()
                .ForMember(dest => dest.AudioFormat, opt => opt.MapFrom(src => src.AudioFormatId.ToString()))
                .ReverseMap();
            CreateMap<GetAllTracksQuery, GetAllTracksParameter>();
            CreateMap<CreateTrackCommand, Track>();
        }
    }
}
