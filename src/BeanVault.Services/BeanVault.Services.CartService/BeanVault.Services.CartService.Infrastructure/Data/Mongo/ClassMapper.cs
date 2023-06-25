
using MongoDB.Bson;

namespace BeanVault.Services.CartService.Infrastructure.Data.Mongo;

public static class ClassMapper
{
  public static void RegisterMappings()
  {
    BsonClassMap.RegisterClassMap<Cart>(
      cm =>
      {
        cm.AutoMap();
        cm.MapIdProperty(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
        cm.MapProperty(c => c.UserId).SetElementName("userId");
        cm.MapProperty(c => c.CouponCode).SetElementName("couponCode");
        cm.MapProperty(c => c.Items).SetElementName("items");
      }
    );

    BsonClassMap.RegisterClassMap<CartItem>(
      cm =>
      {
        cm.AutoMap();
        cm.MapProperty(c => c.ProductId).SetElementName("productId");
        cm.MapProperty(c => c.Count).SetElementName("count");
      }
    );
  }
}