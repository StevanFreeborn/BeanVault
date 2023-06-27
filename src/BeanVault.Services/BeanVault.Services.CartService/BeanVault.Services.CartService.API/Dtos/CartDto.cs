namespace BeanVault.Services.CartService.API.Dtos;

public class CartDto
{
  public string Id { get; set; } = string.Empty;
  public string UserId { get; set; } = string.Empty;
  public string CouponCode { get; set; } = string.Empty;
  public double DiscountAmount { get; set; }
  public List<CartItemDto> Items { get; set; } = new();
  public double Total => Items.Sum(i => i.SubTotal);
  public double DiscountedTotal { get; set; }

  public CartDto()
  {
  }

  public CartDto(Cart cart, List<CartItemDto> cartItems)
  {
    Id = cart.Id;
    UserId = cart.UserId;
    CouponCode = cart.CouponCode;
    Items = cartItems;
  }
}