namespace BeanVault.Services.CartService.Infrastructure.Data.Mongo.Repositories;

public class MongoCartDetailsRepository : ICartDetailsRepository
{
  private readonly MongoDbContext _context;

  public MongoCartDetailsRepository(MongoDbContext context)
  {
    _context = context;
  }
}