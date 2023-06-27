namespace BeanVault.Services.CartService.API.Interfaces;

public interface ICouponService
{
  Task<Coupon?> GetCouponAsync(string couponCode);
}