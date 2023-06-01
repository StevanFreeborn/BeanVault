namespace BeanVault.Services.CouponService.Infrastructure.Data.Mongo.Repositories;

public class MongoCouponRepository : ICouponRepository
{
  private readonly MongoDbContext _context;
  public MongoCouponRepository(MongoDbContext context)
  {
    _context = context;
  }

  public async Task AddCouponAsync(Coupon coupon)
  {
    await _context.Coupons.InsertOneAsync(coupon);
  }

  public async Task<List<Coupon>> GetCouponsAsync()
  {
    return await _context.Coupons.AsQueryable().ToListAsync();
  }
}