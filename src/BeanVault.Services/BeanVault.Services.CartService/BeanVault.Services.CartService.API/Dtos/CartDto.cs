namespace BeanVault.Services.CartService.API.Dtos;

public class CartDto
{
  public string Id { get; set; } = string.Empty;
  public string UserId { get; set; } = string.Empty;
  public string CouponCode { get; set; } = string.Empty;
  public List<CartItemDto> Items { get; set; } = new();
}