namespace BeanVault.Services.CartService.API.Interfaces;

public interface IProductService
{
  Task<CartItemDto?> GetProductAsync(string id);
}