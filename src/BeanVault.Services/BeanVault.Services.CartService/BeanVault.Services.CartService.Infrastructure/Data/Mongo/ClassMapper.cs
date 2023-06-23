
namespace BeanVault.Services.CartService.Infrastructure.Data.Mongo;

public static class ClassMapper
{
  public static void RegisterMappings()
  {
    BsonClassMap.RegisterClassMap<CartHeader>(
      cm =>
      {
        cm.AutoMap();
        cm.MapIdProperty(ch => ch.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
      }
    );

    BsonClassMap.RegisterClassMap<CartDetails>(
      cm =>
      {
        cm.AutoMap();
        cm.MapIdProperty(cd => cd.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
      }
    );
  }
}