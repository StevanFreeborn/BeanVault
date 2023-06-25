namespace BeanVault.Services.CartService.Core.Interfaces;

public interface ICartHeaderRepository
{
  Task<CartHeader?> GetCartHeaderByUserIdAsync(string userId);
  Task<CartHeader> CreateCartHeaderAsync(CartHeader cartHeader);
  Task<CartHeader> GetCartHeaderByIdAsync(string id);
  Task RemoveCartHeaderByIdAsync(string id);
}