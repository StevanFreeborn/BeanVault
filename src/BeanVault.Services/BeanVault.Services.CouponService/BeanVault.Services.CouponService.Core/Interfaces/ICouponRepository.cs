namespace BeanVault.Services.CouponService.Core.Interfaces;

public interface ICouponRepository
{
  Task<Coupon> AddCouponAsync(Coupon coupon);
  Task<List<Coupon>> GetCouponsAsync(CouponQuery query);
  Task<Coupon> GetCouponByIdAsync(string id);
  Task<Coupon> UpdateCouponByIdAsync(Coupon coupon);
  Task DeleteCouponByIdAsync(string id);
}