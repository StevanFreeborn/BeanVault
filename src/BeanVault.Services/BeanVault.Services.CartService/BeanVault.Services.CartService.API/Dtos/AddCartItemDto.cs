namespace BeanVault.Services.CartService.API.Dtos;

public class AddCartItemDto
{
  public string ProductId { get; set; } = string.Empty;
  public int Count { get; set; }

  public CartItem ToCartItem()
  {
    return new CartItem
    {
      ProductId = ProductId,
      Count = Count
    };
  }
}