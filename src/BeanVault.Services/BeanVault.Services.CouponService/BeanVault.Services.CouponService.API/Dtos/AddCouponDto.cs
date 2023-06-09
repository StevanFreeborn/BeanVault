namespace BeanVault.Services.CouponService.API.Dtos;

public class AddCouponDto
{
  [Required]
  public string CouponCode { get; set; } = string.Empty;

  [Required]
  public double DiscountAmount { get; set; }

  [Required]
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