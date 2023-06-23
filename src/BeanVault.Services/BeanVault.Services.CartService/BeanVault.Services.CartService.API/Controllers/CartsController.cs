namespace BeanVault.Services.CartService.API.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/carts")]
[Produces("application/json")]
public class CartsController : ControllerBase
{
  private readonly ICartHeaderRepository _cartHeaderRepository;
  private readonly ICartDetailsRepository _cartDetailsRepository;

  public CartsController(ICartHeaderRepository cartHeaderRepository, ICartDetailsRepository cartDetailsRepository)
  {
    _cartHeaderRepository = cartHeaderRepository;
    _cartDetailsRepository = cartDetailsRepository;
  }

  [HttpPut]
  public async Task<IActionResult> AddOrUpdateCart(CartDto cartDto)
  {
    var existingCartHeader = await _cartHeaderRepository.GetCartHeaderByUserIdAsync(cartDto.CartHeaderDto.UserId);
    var productId = cartDto.CartDetailsDto.First().ProductId;
    var cartDetailsDto = cartDto.CartDetailsDto.First();

    if (existingCartHeader == null)
    {
      var newCartHeader = cartDto.CartHeaderDto.ToCartHeader();
      var createdCartHeader = await _cartHeaderRepository.CreateCartHeaderAsync(newCartHeader);

      cartDetailsDto.CartHeaderId = createdCartHeader.Id;

      var newCartDetails = cartDetailsDto.ToCartDetails();
      var createdCartDetails = await _cartDetailsRepository.CreateCartDetailsAsync(newCartDetails);
      return Ok(new CartDto(createdCartHeader, createdCartDetails));
    }

    var existingCartDetails = await _cartDetailsRepository.GetCartDetailsByProductAndHeaderIdAsync(productId, existingCartHeader.Id);

    if (existingCartDetails == null)
    {
      cartDetailsDto.CartHeaderId = existingCartHeader.Id;
      var newCartDetails = cartDetailsDto.ToCartDetails();
      var createdCartDetails = await _cartDetailsRepository.CreateCartDetailsAsync(newCartDetails);
      return Ok(new CartDto(existingCartHeader, createdCartDetails));
    }

    existingCartDetails.Count += cartDetailsDto.Count;
    var updatedCartDetails = await _cartDetailsRepository.UpdateCartDetailsAsync(existingCartDetails);

    return Ok(new CartDto(existingCartHeader, updatedCartDetails));
  }
}