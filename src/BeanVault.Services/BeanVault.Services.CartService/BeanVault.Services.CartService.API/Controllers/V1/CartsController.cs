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

  // [MapToApiVersion("1.0")]
  // [HttpDelete("{cartId}/item/{cartDetailId}")]
  // public async Task<IActionResult> DeleteCartDetail(string cartId, string cartDetailId)
  // {
  //   var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
  //   var cartHeader = await _cartHeaderRepository.GetCartHeaderByIdAsync(cartId);

  //   if (userId?.Value != cartHeader.UserId)
  //   {
  //     return Forbid();
  //   }

  //   await _cartDetailsRepository.RemoveCartDetailByIdAsync(cartDetailId);

  //   var cartDetails = await _cartDetailsRepository.GetCartDetailsByCartHeaderIdAsync(cartHeader.Id);

  //   if (cartDetails.Any() is false)
  //   {
  //     await _cartHeaderRepository.RemoveCartHeaderByIdAsync(cartHeader.Id);
  //   }

  //   return NoContent();
  // }

  // [MapToApiVersion("1.0")]
  // [HttpPut]
  // public async Task<IActionResult> AddOrUpdateCart(CartDto cartDto)
  // {
  //   var existingCartHeader = await _cartHeaderRepository.GetCartHeaderByUserIdAsync(cartDto.CartHeaderDto.UserId);
  //   var productId = cartDto.CartDetailsDto.First().ProductId;
  //   var cartDetailsDto = cartDto.CartDetailsDto.First();

  //   if (existingCartHeader is null)
  //   {
  //     var newCartHeader = cartDto.CartHeaderDto.ToCartHeader();
  //     var createdCartHeader = await _cartHeaderRepository.CreateCartHeaderAsync(newCartHeader);

  //     cartDetailsDto.CartHeaderId = createdCartHeader.Id;

  //     var newCartDetails = cartDetailsDto.ToCartDetails();
  //     var createdCartDetails = await _cartDetailsRepository.CreateCartDetailsAsync(newCartDetails);
  //     return Ok(new CartDto(createdCartHeader, createdCartDetails));
  //   }

  //   var existingCartDetails = await _cartDetailsRepository.GetCartDetailsByProductAndHeaderIdAsync(productId, existingCartHeader.Id);

  //   if (existingCartDetails is null)
  //   {
  //     cartDetailsDto.CartHeaderId = existingCartHeader.Id;
  //     var newCartDetails = cartDetailsDto.ToCartDetails();
  //     var createdCartDetails = await _cartDetailsRepository.CreateCartDetailsAsync(newCartDetails);
  //     return Ok(new CartDto(existingCartHeader, createdCartDetails));
  //   }

  //   existingCartDetails.Count += cartDetailsDto.Count;
  //   var updatedCartDetails = await _cartDetailsRepository.UpdateCartDetailsAsync(existingCartDetails);

  //   return Ok(new CartDto(existingCartHeader, updatedCartDetails));
  // }
}