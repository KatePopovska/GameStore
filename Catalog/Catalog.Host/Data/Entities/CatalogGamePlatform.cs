namespace Catalog.Host.Data.Entities
{
    public class CatalogGamePlatform
    {
        public int CatalogGameId { get; set; }
        public CatalogGame CatalogGame { get; set; }

        public int CatalogPlatformId { get; set; }
        public CatalogPlatform CatalogPlatform { get; set; }
    }
}
