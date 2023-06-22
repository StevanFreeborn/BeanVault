namespace BeanVault.Services.CouponService.Infrastructure.Data.Mongo;

public static class ClassMapper
{
  public static void RegisterMappings()
  {
    BsonClassMap.RegisterClassMap<Coupon>(
    cm =>
    {
      cm.AutoMap();
      cm.MapIdProperty(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
      cm.MapProperty(c => c.CouponCode).SetElementName("couponCode");
      cm.MapProperty(c => c.DiscountAmount).SetElementName("discountAmount");
      cm.MapProperty(c => c.MinAmount).SetElementName("minAmount");
    }
  );
  }
}