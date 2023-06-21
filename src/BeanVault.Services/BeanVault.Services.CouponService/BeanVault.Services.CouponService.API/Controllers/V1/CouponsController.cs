namespace BeanVault.Services.CouponService.API.Controllers;

[Authorize]
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

  /// <summary>
  /// Get coupons
  /// </summary>
  [MapToApiVersion("1.0")]
  [HttpGet]
  [ProducesResponseType(typeof(List<CouponDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetCoupons([FromQuery] CouponQuery query)
  {
    var coupons = await _couponRepository.GetCouponsAsync(query);
    var couponDtos = coupons.Select(c => new CouponDto(c)).ToList();
    return Ok(couponDtos);
  }

  /// <summary>
  /// Get a coupon by id
  /// </summary>
  [MapToApiVersion("1.0")]
  [HttpGet("{id}")]
  [ProducesResponseType(typeof(List<CouponDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetCouponById(string id)
  {
    var coupon = await _couponRepository.GetCouponByIdAsync(id);
    return Ok(new CouponDto(coupon));
  }

  /// <summary>
  /// Add coupon
  /// </summary>
  [Authorize(Roles = "admin")]
  [MapToApiVersion("1.0")]
  [HttpPost]
  [ProducesResponseType(typeof(CouponDto), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> AddCoupon(AddCouponDto addCouponDto)
  {
    var coupon = addCouponDto.ToCoupon();
    var newCoupon = await _couponRepository.AddCouponAsync(coupon);
    return CreatedAtAction(
      nameof(GetCouponById),
      new { id = newCoupon.Id },
      new CouponDto(newCoupon)
    );
  }

  /// <summary>
  /// Update a coupon by id
  /// </summary>
  [Authorize(Roles = "admin")]
  [MapToApiVersion("1.0")]
  [HttpPut]
  [ProducesResponseType(typeof(CouponDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> UpdateCoupon(CouponDto couponDto)
  {
    var updatedCoupon = await _couponRepository.UpdateCouponByIdAsync(couponDto.ToCoupon());
    return Ok(new CouponDto(updatedCoupon));
  }

  /// <summary>
  /// Delete a coupon by id
  /// </summary>
  [Authorize(Roles = "admin")]
  [MapToApiVersion("1.0")]
  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> DeleteCouponById(string id)
  {
    await _couponRepository.DeleteCouponByIdAsync(id);
    return NoContent();
  }
}