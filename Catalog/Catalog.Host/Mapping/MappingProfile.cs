using AutoMapper;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dto;

namespace Catalog.Host.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<CatalogGame, CatalogGameDto>()
                .ForMember("PictureUrl", x => x.MapFrom<CatalogGamePictureResolver, string>(c => c.PictureFileName)).ReverseMap();

            CreateMap<CatalogGenre, CatalogGenreDto>();
            CreateMap<CatalogPlatform, CatalogPlatformDto>();
            CreateMap<CatalogPublisher, CatalogPublisherDto>();
        }
    }
}
