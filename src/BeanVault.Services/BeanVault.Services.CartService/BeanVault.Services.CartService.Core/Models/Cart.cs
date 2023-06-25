namespace BeanVault.Services.CartService.Core.Models;

public class Cart
{
  public string Id { get; set; } = string.Empty;
  public string UserId { get; set; } = string.Empty;
  public string CouponCode { get; set; } = string.Empty;
  public List<CartItem> Items { get; set; } = new();
}