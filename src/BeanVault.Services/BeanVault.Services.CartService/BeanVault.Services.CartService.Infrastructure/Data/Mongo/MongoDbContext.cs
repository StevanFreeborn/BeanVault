namespace BeanVault.Services.CartService.Infrastructure.Data.Mongo;

public class MongoDbContext
{
  private const string CartHeaderCollectionName = "cartHeader";
  private const string CartDetailsCollectionName = "cartDetails";
  private readonly MongoDbOptions _options;
  public IMongoCollection<CartHeader> CartHeaders { get; set; }
  public IMongoCollection<CartDetails> CartDetails { get; set; }

  public MongoDbContext(IOptions<MongoDbOptions> options)
  {
    _options = options.Value;
    IMongoClient client = new MongoClient(_options.ConnectionString);
    IMongoDatabase database = client.GetDatabase(_options.DatabaseName);
    CartHeaders = database.GetCollection<CartHeader>(CartHeaderCollectionName);
    CartDetails = database.GetCollection<CartDetails>(CartDetailsCollectionName);
  }
}