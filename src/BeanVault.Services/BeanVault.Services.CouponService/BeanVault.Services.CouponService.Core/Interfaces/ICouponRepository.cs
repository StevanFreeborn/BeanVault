namespace BeanVault.Services.CouponService.Core.Interfaces;

public interface ICouponRepository
{
  Task AddCouponAsync(Coupon coupon);
  Task<List<Coupon>> GetCouponsAsync();
  Task<Coupon?> GetCouponByIdAsync(string id);
}