namespace BeanVault.Services.CouponService.Infrastructure.Data.Mongo;

public class MongoDbContext
{
  private const string CouponCollectionName = "coupons";
  private readonly MongoDbOptions _options;
  public IMongoCollection<Coupon> Coupons { get; set; }

  public MongoDbContext(IOptions<MongoDbOptions> options)
  {
    _options = options.Value;
    IMongoClient client = new MongoClient(_options.ConnectionString);
    IMongoDatabase database = client.GetDatabase(_options.DatabaseName);
    Coupons = database.GetCollection<Coupon>(CouponCollectionName);
  }
}