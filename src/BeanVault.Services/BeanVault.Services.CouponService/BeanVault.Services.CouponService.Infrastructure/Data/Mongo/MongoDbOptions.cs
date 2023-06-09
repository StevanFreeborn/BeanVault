namespace BeanVault.Services.CouponService.Infrastructure.Data.Mongo;

public class MongoDbOptions
{
  public string ConnectionString { get; set; } = string.Empty;
  public string DatabaseName { get; set; } = string.Empty;
}