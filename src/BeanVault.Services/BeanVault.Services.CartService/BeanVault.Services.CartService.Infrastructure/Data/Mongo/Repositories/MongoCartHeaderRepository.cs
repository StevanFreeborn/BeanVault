namespace BeanVault.Services.CartService.Infrastructure.Data.Mongo.Repositories;

public class MongoCartHeaderRepository : ICartHeaderRepository
{
  private readonly MongoDbContext _context;

  public MongoCartHeaderRepository(MongoDbContext context)
  {
    _context = context;
  }

  // TODO: Implement
  public Task<CartHeader> CreateCartHeaderAsync(CartHeader cartHeader)
  {
    throw new NotImplementedException();
  }

  public Task<CartHeader?> GetCartHeaderByUserIdAsync(string userId)
  {
    throw new NotImplementedException();
  }
}