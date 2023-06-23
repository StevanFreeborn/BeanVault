namespace BeanVault.Services.CartService.Infrastructure.Data.Mongo.Repositories;

public class MongoCartDetailsRepository : ICartDetailsRepository
{
  private readonly MongoDbContext _context;

  public MongoCartDetailsRepository(MongoDbContext context)
  {
    _context = context;
  }

  // TODO: Implement
  public Task<CartDetails> CreateCartDetailsAsync(CartDetails cartDetail)
  {
    throw new NotImplementedException();
  }

  public Task<CartDetails?> GetCartDetailsByProductAndHeaderIdAsync(string productId, string headerId)
  {
    throw new NotImplementedException();
  }

  public Task<CartDetails> UpdateCartDetailsAsync(CartDetails cartDetail)
  {
    throw new NotImplementedException();
  }
}