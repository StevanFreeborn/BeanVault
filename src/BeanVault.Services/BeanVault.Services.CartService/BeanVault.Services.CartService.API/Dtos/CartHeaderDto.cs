namespace BeanVault.Services.CartService.API.Dtos;

public class CartHeaderDto
{
  public string Id { get; set; } = string.Empty;
  public string UserId { get; set; } = string.Empty;
  public string CouponCode { get; set; } = string.Empty;
  public double Discount { get; set; }
  public double CartTotal { get; set; }

  public CartHeaderDto()
  {
  }

  public CartHeaderDto(CartHeader cartHeader)
  {
    Id = cartHeader.Id;
    UserId = cartHeader.UserId;
    CouponCode = cartHeader.CouponCode;
  }

  public CartHeader ToCartHeader()
  {
    return new()
    {
      Id = Id,
      UserId = UserId,
      CouponCode = CouponCode
    };
  }
}