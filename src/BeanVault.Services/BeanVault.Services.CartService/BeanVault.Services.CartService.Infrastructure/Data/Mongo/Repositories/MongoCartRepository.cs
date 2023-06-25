namespace BeanVault.Services.CartService.Infrastructure.Data.Mongo.Repositories;

public class MongoCartRepository : ICartRepository
{
  private readonly MongoDbContext _context;

  public MongoCartRepository(MongoDbContext context)
  {
    _context = context;
  }

  public async Task<Cart> CreateCartAsync(Cart cart)
  {
    await _context.Carts.InsertOneAsync(cart);
    return cart;
  }

  public async Task<Cart?> GetCartByUserIdAsync(string userId)
  {
    return await _context.Carts
    .Find(c => c.UserId == userId)
    .FirstOrDefaultAsync();
  }

  public async Task<Cart> UpdateCartAsync(Cart cart)
  {
    var updatedCart = await _context.Carts.FindOneAndReplaceAsync(
      c => c.Id == cart.Id,
      cart,
      new()
      {
        ReturnDocument = ReturnDocument.After,
      }
    );

    if (updatedCart is null)
    {
      throw new ModelNotFoundException($"Unable to update cart with id: {cart.Id}");
    }

    return updatedCart;
  }
}