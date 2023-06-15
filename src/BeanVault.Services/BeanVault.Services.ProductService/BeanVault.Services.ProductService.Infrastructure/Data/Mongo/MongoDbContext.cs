namespace BeanVault.Services.ProductService.Infrastructure.Data.Mongo;

public class MongoDbContext
{
  private const string ProductCollectionName = "products";
  private readonly MongoDbOptions _options;
  public IMongoCollection<Product> Products { get; set; }

  public MongoDbContext(IOptions<MongoDbOptions> options)
  {
    _options = options.Value;
    IMongoClient client = new MongoClient(_options.ConnectionString);
    IMongoDatabase database = client.GetDatabase(_options.DatabaseName);
    Products = database.GetCollection<Product>(ProductCollectionName);
  }
}