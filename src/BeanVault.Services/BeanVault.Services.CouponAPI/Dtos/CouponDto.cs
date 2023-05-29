namespace BeanVault.Services.CouponAPI.Dtos;

public class CouponDto
{
  public int Id { get; set; }
  public string CouponCode { get; set; } = string.Empty;
  public double DiscountAmount { get; set; }
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
}