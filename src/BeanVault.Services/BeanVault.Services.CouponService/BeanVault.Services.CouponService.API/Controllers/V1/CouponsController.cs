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
  public async Task<IActionResult> GetCouponsAsync([FromQuery] CouponQuery query)
  {
    var coupons = await _couponRepository.GetCouponsAsync(query);
    var couponDtos = coupons.Select(c => new CouponDto(c)).ToList();
    return Ok(couponDtos);
  }

  [MapToApiVersion("1.0")]
  [HttpGet("{id}")]
  [ProducesResponseType(typeof(List<CouponDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetCouponByIdAsync(string id)
  {
    var coupon = await _couponRepository.GetCouponByIdAsync(id);

    if (coupon is null)
    {
      return NotFound();
    }

    return Ok(new CouponDto(coupon));
  }

  [MapToApiVersion("1.0")]
  [HttpPost]
  [ProducesResponseType(typeof(CouponDto), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> AddCouponAsync(AddCouponDto addCouponDto)
  {
    var coupon = addCouponDto.ToCoupon();
    var newCoupon = await _couponRepository.AddCouponAsync(coupon);
    return Created($"/api/coupons/{newCoupon.Id}", new CouponDto(newCoupon));
  }

  [MapToApiVersion("1.0")]
  [HttpPut]
  [ProducesResponseType(typeof(CouponDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> UpdateCouponAsync(CouponDto couponDto)
  {
    var updatedCoupon = await _couponRepository.UpdateCouponByIdAsync(couponDto.ToCoupon());

    if (updatedCoupon is null)
    {
      return NotFound();
    }

    return Ok(new CouponDto(updatedCoupon));
  }

  [MapToApiVersion("1.0")]
  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> DeleteCouponByIdAsync(string id)
  {
    var deletedCoupon = await _couponRepository.DeleteCouponByIdAsync(id);

    if (deletedCoupon is null)
    {
      return NotFound();
    }

    return NoContent();
  }
}