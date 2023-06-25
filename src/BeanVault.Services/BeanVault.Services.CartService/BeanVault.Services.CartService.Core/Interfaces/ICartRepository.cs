namespace BeanVault.Services.CartService.Core.Interfaces;

public interface ICartRepository
{
  Task<Cart?> GetCartByUserIdAsync(string userId);
  Task<Cart> CreateCartAsync(Cart cart);
  Task<Cart> UpdateCartAsync(Cart cart);
}