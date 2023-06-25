namespace BeanVault.Services.CartService.Core.Interfaces;

public interface ICartDetailsRepository
{
  Task<CartDetails?> GetCartDetailsByProductAndHeaderIdAsync(string productId, string headerId);
  Task<CartDetails> CreateCartDetailsAsync(CartDetails cartDetail);
  Task<CartDetails> UpdateCartDetailsAsync(CartDetails cartDetail);
  Task<List<CartDetails>> GetCartDetailsByCartHeaderIdAsync(string cartHeaderId);
  Task RemoveCartDetailByIdAsync(string id);
}