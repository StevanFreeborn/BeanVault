
namespace BeanVault.Services.ProductService.Infrastructure.Data.Mongo;

public static class ClassMapper
{
  public static void RegisterMappings()
  {
    BsonClassMap.RegisterClassMap<Product>(
      cm =>
      {
        cm.AutoMap();
        cm.MapIdProperty(p => p.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
        cm.MapProperty(p => p.Name).SetElementName("name");
        cm.MapProperty(p => p.Price).SetElementName("price");
        cm.MapProperty(p => p.Description).SetElementName("description");
        cm.MapProperty(p => p.CategoryName).SetElementName("categoryName");
        cm.MapProperty(p => p.ImageUrl).SetElementName("imageUrl");
      }
    );
  }
}