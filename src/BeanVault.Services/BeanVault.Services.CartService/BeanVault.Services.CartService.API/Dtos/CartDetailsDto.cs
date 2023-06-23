namespace BeanVault.Services.CartService.API.Dtos;

public class CartDetailsDto
{
  public string Id { get; set; } = string.Empty;
  public string CartHeaderId { get; set; } = string.Empty;
  public string ProductId { get; set; } = string.Empty;
  public int Count { get; set; }

  public CartDetailsDto()
  {
  }

  public CartDetailsDto(CartDetails cartDetails)
  {
    Id = cartDetails.Id;
    CartHeaderId = cartDetails.CartHeaderId;
    ProductId = cartDetails.ProductId;
    Count = cartDetails.Count;
  }

  public CartDetails ToCartDetails()
  {
    return new()
    {
      Id = Id,
      CartHeaderId = CartHeaderId,
      ProductId = ProductId,
      Count = Count,
    };
  }
}