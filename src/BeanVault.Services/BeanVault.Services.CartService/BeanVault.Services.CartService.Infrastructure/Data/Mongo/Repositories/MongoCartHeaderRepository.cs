namespace BeanVault.Services.CartService.Infrastructure.Data.Mongo.Repositories;

public class MongoCartHeaderRepository : ICartHeaderRepository
{
  private readonly MongoDbContext _context;

  public MongoCartHeaderRepository(MongoDbContext context)
  {
    _context = context;
  }
}