using BeanVault.Services.CouponService.Core.Exceptions;

namespace BeanVault.Services.CouponService.Infrastructure.Data.Mongo.Repositories;

public class MongoCouponRepository : ICouponRepository
{
  private readonly MongoDbContext _context;
  public MongoCouponRepository(MongoDbContext context)
  {
    _context = context;
  }

  public async Task<Coupon> AddCouponAsync(Coupon coupon)
  {
    await _context.Coupons.InsertOneAsync(coupon);
    return coupon;
  }

  public async Task<List<Coupon>> GetCouponsAsync(CouponQuery query)
  {
    var coupons = _context.Coupons.AsQueryable();

    if (query.CouponCode != string.Empty)
    {
      coupons = coupons.Where(c => c.CouponCode.ToLower() == query.CouponCode.ToLower());
    }

    return await coupons.ToListAsync();
  }

  public async Task<Coupon?> GetCouponByIdAsync(string id)
  {
    return await _context.Coupons.Find(c => c.Id == id).FirstOrDefaultAsync();
  }

  public async Task<Coupon> UpdateCouponByIdAsync(Coupon coupon)
  {
    var updatedCoupon = await _context.Coupons.FindOneAndReplaceAsync(
      c => c.Id == coupon.Id,
      coupon,
      new()
      {
        ReturnDocument = ReturnDocument.After,
      }
    );

    if (updatedCoupon is null)
    {
      throw new ModelNotFoundException($"Unable to update coupon with id: {coupon.Id}");
    }

    return updatedCoupon;
  }

  public async Task DeleteCouponByIdAsync(string id)
  {
    var deletedCoupon = await _context.Coupons.FindOneAndDeleteAsync(c => c.Id == id);

    if (deletedCoupon is null)
    {
      throw new ModelNotFoundException($"Unable to delete coupon with id: {id}");
    }
  }
}