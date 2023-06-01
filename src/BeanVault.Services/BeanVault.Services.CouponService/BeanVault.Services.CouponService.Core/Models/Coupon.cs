namespace BeanVault.Services.CouponService.Core.Models;

public class Coupon
{
  public string Id { get; set; } = string.Empty;
  public string CouponCode { get; set; } = string.Empty;
  public double DiscountAmount { get; set; }
  public int MinAmount { get; set; }
}