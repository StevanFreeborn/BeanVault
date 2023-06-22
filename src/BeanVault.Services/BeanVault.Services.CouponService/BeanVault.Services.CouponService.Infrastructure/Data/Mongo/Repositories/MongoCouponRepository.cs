using BeanVault.Services.CouponService.Core.Exceptions;

namespace BeanVault.Services.CouponService.Infrastructure.Data.Mongo.Repositories;

public class MongoCouponRepository : ICouponRepository
{
  private readonly MongoDbContext _context;
  public MongoCouponRepository(MongoDbContext context)
  {
    _context = context;
  }

  /// <summary>
  /// Adds the given coupon.
  /// </summary>
  /// <param name="coupon"></param>
  /// <returns>The newly created coupon.</returns>
  public async Task<Coupon> AddCouponAsync(Coupon coupon)
  {
    await _context.Coupons.InsertOneAsync(coupon);
    return coupon;
  }

  /// <summary>
  /// Gets a list of coupons based on given query filter.
  /// </summary>
  /// <param name="query"></param>
  /// <returns>A list of coupons</returns>
  public async Task<List<Coupon>> GetCouponsAsync(CouponQuery query)
  {
    var coupons = _context.Coupons.AsQueryable();

    if (query.CouponCode != string.Empty)
    {
      coupons = coupons.Where(c => c.CouponCode.ToLower() == query.CouponCode.ToLower());
    }

    return await coupons.ToListAsync();
  }

  /// <summary>
  /// Gets a coupon with the given id.
  /// </summary>
  /// <param name="id"></param>
  /// <returns>A coupon</returns>
  /// <exception cref="ModelNotFoundException"></exception>
  public async Task<Coupon> GetCouponByIdAsync(string id)
  {
    var coupon = await _context.Coupons.Find(c => c.Id == id).FirstOrDefaultAsync();

    if (coupon is null)
    {
      throw new ModelNotFoundException($"Unable to find coupon with id: {id}");
    }

    return coupon;
  }

  /// <summary>
  /// Updates the coupon with the given state.
  /// </summary>
  /// <param name="coupon"></param>
  /// <returns>The updated coupon.</returns>
  /// <exception cref="ModelNotFoundException"></exception>
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

  /// <summary>
  /// Deletes the coupon with the given id.
  /// </summary>
  /// <param name="id"></param>
  /// <exception cref="ModelNotFoundException"></exception>
  public async Task DeleteCouponByIdAsync(string id)
  {
    var deletedCoupon = await _context.Coupons.FindOneAndDeleteAsync(c => c.Id == id);

    if (deletedCoupon is null)
    {
      throw new ModelNotFoundException($"Unable to delete coupon with id: {id}");
    }
  }
}