namespace BeanVault.Services.CartService.Infrastructure.Data.Mongo.Repositories;

public class MongoCartDetailsRepository : ICartDetailsRepository
{
  private readonly MongoDbContext _context;

  public MongoCartDetailsRepository(MongoDbContext context)
  {
    _context = context;
  }

  public async Task<CartDetails> CreateCartDetailsAsync(CartDetails cartDetail)
  {
    await _context.CartDetails.InsertOneAsync(cartDetail);
    return cartDetail;
  }

  public async Task<List<CartDetails>> GetCartDetailsByCartHeaderIdAsync(string cartHeaderId)
  {
    return await _context.CartDetails
    .Find(cd => cd.CartHeaderId == cartHeaderId)
    .ToListAsync();
  }


  public async Task<CartDetails?> GetCartDetailsByProductAndHeaderIdAsync(string productId, string headerId)
  {
    return await _context
    .CartDetails
    .Find(cd => cd.ProductId == productId && cd.CartHeaderId == headerId)
    .FirstOrDefaultAsync();
  }

  public async Task RemoveCartDetailByIdAsync(string id)
  {
    var deletedCartDetail = await _context.CartDetails.FindOneAndDeleteAsync(cd => cd.Id == id);

    if (deletedCartDetail is null)
    {
      throw new ApplicationException($"Unable to delete cart detail with id: {id}");
    }
  }


  public async Task<CartDetails> UpdateCartDetailsAsync(CartDetails cartDetail)
  {
    var updatedCartDetail = await _context.CartDetails.FindOneAndReplaceAsync(
      cd => cd.Id == cartDetail.Id,
      cartDetail,
      new()
      {
        ReturnDocument = ReturnDocument.After,
      }
    );

    if (updatedCartDetail is null)
    {
      throw new ApplicationException($"Unable to update cart detail with id: {cartDetail.Id}");
    }

    return updatedCartDetail;
  }
}