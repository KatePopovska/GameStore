using AutoMapper;
using Catalog.Host.Configurations;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dto;
using Microsoft.Extensions.Options;

namespace Catalog.Host.Mapping
{
    public class CatalogGamePictureResolver : IMemberValueResolver<CatalogGame, CatalogGameDto, string, object>
    {
        private readonly CatalogConfig _config;
        
        public CatalogGamePictureResolver(IOptionsSnapshot<CatalogConfig> config)
        { 
            _config = config.Value;
        }
        public object Resolve(CatalogGame source, CatalogGameDto destination, string sourceMember, object destMember, ResolutionContext context)
        {
            return $"{_config.CdnHost}/{_config.ImgUrl}/{sourceMember}";
        }
    }
}
