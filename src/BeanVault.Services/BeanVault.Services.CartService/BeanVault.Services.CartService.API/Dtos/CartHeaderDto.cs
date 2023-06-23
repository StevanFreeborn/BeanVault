namespace BeanVault.Services.CartService.API.Dtos;

public class CartHeaderDto
{
  public string Id { get; set; } = string.Empty;
  public string UserId { get; set; } = string.Empty;
  public string CouponCode { get; set; } = string.Empty;
  public double Discount { get; set; }
  public double CartTotal { get; set; }
}