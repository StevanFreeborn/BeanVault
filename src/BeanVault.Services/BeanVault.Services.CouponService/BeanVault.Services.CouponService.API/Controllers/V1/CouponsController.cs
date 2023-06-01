namespace BeanVault.Services.CouponService.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/coupons")]
[Produces("application/json")]
public class CouponsController : ControllerBase
{
  private readonly ICouponRepository _couponRepository;
  public CouponsController(ICouponRepository couponRepository)
  {
    _couponRepository = couponRepository;
  }

  [MapToApiVersion("1.0")]
  [HttpGet]
  [ProducesResponseType(typeof(List<CouponDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetCouponsAsync()
  {
    var coupons = await _couponRepository.GetCouponsAsync();
    var couponDtos = coupons.Select(c => new CouponDto(c)).ToList();
    return Ok(couponDtos);
  }

  [MapToApiVersion("1.0")]
  [HttpPost]
  public async Task<IActionResult> AddCouponAsync(AddCouponDto addCouponDto)
  {
    var coupon = addCouponDto.ToCoupon();
    await _couponRepository.AddCouponAsync(coupon);
    return Ok();
  }
}