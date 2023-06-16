namespace BeanVault.Services.CouponService.API.Dtos;

public class CouponDto
{
  [Required]
  public string Id { get; set; } = string.Empty;

  [Required]
  [MaxLength(150)]
  public string CouponCode { get; set; } = string.Empty;

  [Required]
  [Range(1, double.MaxValue)]
  public double DiscountAmount { get; set; }

  [Required]
  [Range(1, double.MaxValue)]
  public double MinAmount { get; set; }

  public CouponDto()
  {
  }

  public CouponDto(Coupon coupon)
  {
    Id = coupon.Id;
    CouponCode = coupon.CouponCode;
    DiscountAmount = coupon.DiscountAmount;
    MinAmount = coupon.MinAmount;
  }

  public Coupon ToCoupon()
  {
    return new Coupon
    {
      Id = Id,
      CouponCode = CouponCode,
      DiscountAmount = DiscountAmount,
      MinAmount = MinAmount,
    };
  }
}