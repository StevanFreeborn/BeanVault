namespace BeanVault.Services.CartService.API.Dtos;

public class AddCartItemDto
{
  [Required]
  public string ProductId { get; set; } = string.Empty;

  [Required]
  [Range(1, int.MaxValue)]
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