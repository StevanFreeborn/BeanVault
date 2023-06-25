namespace BeanVault.Services.CartService.Infrastructure.Data.Mongo.Repositories;

public class MongoCartHeaderRepository : ICartHeaderRepository
{
  private readonly MongoDbContext _context;

  public MongoCartHeaderRepository(MongoDbContext context)
  {
    _context = context;
  }

  public async Task<CartHeader> CreateCartHeaderAsync(CartHeader cartHeader)
  {
    await _context.CartHeaders.InsertOneAsync(cartHeader);
    return cartHeader;
  }

  public async Task<CartHeader> GetCartHeaderByIdAsync(string id)
  {
    var cartHeader = await _context.CartHeaders.Find(ch => ch.Id == id).FirstOrDefaultAsync();

    if (cartHeader is null)
    {
      throw new ApplicationException($"Unable to find cart header with id: {id}");
    }

    return cartHeader;
  }

  public async Task<CartHeader?> GetCartHeaderByUserIdAsync(string userId)
  {
    return await _context.CartHeaders.Find(ch => ch.UserId == userId).FirstOrDefaultAsync();
  }

  public async Task RemoveCartHeaderByIdAsync(string id)
  {
    var deletedCartHeader = await _context.CartHeaders.FindOneAndDeleteAsync(ch => ch.Id == id);

    if (deletedCartHeader is null)
    {
      throw new ApplicationException($"Unable to delete cart header with id: {id}");
    }
  }
}