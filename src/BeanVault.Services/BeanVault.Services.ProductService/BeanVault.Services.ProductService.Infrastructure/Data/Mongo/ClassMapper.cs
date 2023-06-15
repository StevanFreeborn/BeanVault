
namespace BeanVault.Services.ProductService.Infrastructure.Data.Mongo;

public static class ClassMapper
{
  public static void RegisterMappings()
  {
    BsonClassMap.RegisterClassMap<Product>(
    cm =>
    {
      cm.AutoMap();
      cm.MapIdProperty(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
      cm.MapProperty(c => c.Name).SetElementName("name");
      cm.MapProperty(c => c.Price).SetElementName("price");
      cm.MapProperty(c => c.Description).SetElementName("description");
      cm.MapProperty(c => c.CategoryName).SetElementName("categoryName");
      cm.MapProperty(c => c.ImageUrl).SetElementName("imageUrl");
    }
  );
  }
}