namespace BeanVault.Services.CouponService.API.Dtos;

public class AddCouponDto
{
  public string CouponCode { get; set; } = string.Empty;
  public double DiscountAmount { get; set; }
  public int MinAmount { get; set; }

  public AddCouponDto()
  {
  }

  public Coupon ToCoupon()
  {
    return new Coupon
    {
      CouponCode = CouponCode,
      DiscountAmount = DiscountAmount,
      MinAmount = MinAmount,
    };
  }
}