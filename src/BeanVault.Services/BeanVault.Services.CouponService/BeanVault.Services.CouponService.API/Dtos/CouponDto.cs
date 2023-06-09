using System.Text.Json.Serialization;

namespace BeanVault.Services.CouponService.API.Dtos;

public class CouponDto
{
  [Required]
  public string Id { get; set; } = string.Empty;

  [Required]
  public string CouponCode { get; set; } = string.Empty;

  [Required]
  public double DiscountAmount { get; set; }

  [Required]
  public int MinAmount { get; set; }

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