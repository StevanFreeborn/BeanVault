namespace BeanVault.Services.CartService.API.Dtos;

public class CartDto
{
  public CartHeaderDto CartHeaderDto { get; set; } = new();
  public List<CartDetailsDto> CartDetailsDto { get; set; } = new();

  public CartDto()
  {
  }

  public CartDto(CartHeader cartHeader, CartDetails cartDetails)
  {
    CartHeaderDto = new(cartHeader);
    CartDetailsDto = new()
    {
      new CartDetailsDto(cartDetails)
    };
  }
}