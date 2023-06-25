namespace BeanVault.Services.CartService.Core.Models;

public class CartItem
{
  public string Id { get; set; } = string.Empty;
  public string ProductId { get; set; } = string.Empty;
  public int Count { get; set; }
}