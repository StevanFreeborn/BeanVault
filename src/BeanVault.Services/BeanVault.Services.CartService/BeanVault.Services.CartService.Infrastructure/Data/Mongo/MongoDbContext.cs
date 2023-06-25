namespace BeanVault.Services.CartService.Infrastructure.Data.Mongo;

public class MongoDbContext
{
  private const string CartCollectionName = "carts";
  private readonly MongoDbOptions _options;
  public IMongoCollection<Cart> Carts { get; set; }

  public MongoDbContext(IOptions<MongoDbOptions> options)
  {
    _options = options.Value;
    IMongoClient client = new MongoClient(_options.ConnectionString);
    IMongoDatabase database = client.GetDatabase(_options.DatabaseName);
    Carts = database.GetCollection<Cart>(CartCollectionName);
  }
}