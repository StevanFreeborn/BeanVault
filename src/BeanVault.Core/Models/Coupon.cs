namespace BeanVault.Core.Models;

public class Coupon
{
  public int Id { get; set; }
  public string CouponCode { get; set; } = string.Empty;
  public double DiscountAmount { get; set; }
  public int MinAmount { get; set; }
}
