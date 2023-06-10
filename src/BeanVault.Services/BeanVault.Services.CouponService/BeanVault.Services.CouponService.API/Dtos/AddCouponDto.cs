namespace BeanVault.Services.CouponService.API.Dtos;

public class AddCouponDto
{
  [Required]
  [MaxLength(150)]
  public string CouponCode { get; set; } = string.Empty;

  [Required]
  [Range(1, double.MaxValue)]
  public double DiscountAmount { get; set; }

  [Required]
  [Range(1, double.MaxValue)]
  public double MinAmount { get; set; }

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