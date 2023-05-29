namespace BeanVault.Services.CouponService.Infrastructure.Data.Mongo;

public class MongoDbContext
{
  private const string CouponCollectionName = "coupons";
  public IMongoCollection<Coupon> Coupons { get; set; }

  public MongoDbContext()
  {
    IMongoClient client = new MongoClient("connection string");
    IMongoDatabase database = client.GetDatabase("database name");
    Coupons = database.GetCollection<Coupon>(CouponCollectionName);
  }
}