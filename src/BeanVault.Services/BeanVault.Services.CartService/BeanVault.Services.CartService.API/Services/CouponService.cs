namespace BeanVault.Services.CartService.API.Services;

public class CouponService : ICouponService
{
  private readonly ServiceUrls _serviceUrls;
  private readonly HttpClient _client;

  public CouponService(IOptions<ServiceUrls> options, IHttpClientFactory httpClientFactory)
  {
    _serviceUrls = options.Value;
    _client = httpClientFactory.CreateClient("authClient");
    _client.BaseAddress = new Uri(_serviceUrls.CouponService);
  }

  public async Task<Coupon?> GetCouponAsync(string couponCode)
  {
    var coupons = await _client.GetFromJsonAsync<List<Coupon>>(
      $"{_serviceUrls.CouponService}/api/coupons?couponCode={couponCode}"
    );

    return coupons?.FirstOrDefault();
  }
}