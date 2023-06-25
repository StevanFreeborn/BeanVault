namespace BeanVault.Services.CartService.API.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/carts")]
[Produces("application/json")]
public class CartsController : ControllerBase
{
  private readonly ICartRepository _cartRepository;

  public CartsController(ICartRepository cartRepository)
  {
    _cartRepository = cartRepository;
  }

  [MapToApiVersion("1.0")]
  [HttpGet]
  public async Task<IActionResult> GetCart(string userId)
  {
    var cart = await _cartRepository.GetCartByUserIdAsync(userId);

    if (cart is null)
    {
      return NotFound($"No cart found for user with id: {userId}");
    }

    return Ok(cart);
  }

  [MapToApiVersion("1.0")]
  [HttpPut("{userId}/apply-coupon")]
  public async Task<IActionResult> UpdateCart(string userId, UpdateCartCouponDto updateCartCouponDto)
  {
    var cart = await _cartRepository.GetCartByUserIdAsync(userId);

    if (cart is null)
    {
      return NotFound($"No cart found for user with id: {userId}");
    }

    cart.CouponCode = updateCartCouponDto.CouponCode;
    var updatedCart = await _cartRepository.UpdateCartAsync(cart);

    return Ok(updatedCart);
  }

  [MapToApiVersion("1.0")]
  [HttpDelete("{userId}/items/{productId}")]
  public async Task<IActionResult> DeleteCartItem(string userId, string productId)
  {
    var cart = await _cartRepository.GetCartByUserIdAsync(userId);

    if (cart is null)
    {
      return NotFound($"No cart found for user with id: {userId}");
    }

    var itemToRemove = cart.Items.FirstOrDefault(ci => ci.ProductId == productId);

    if (itemToRemove is null)
    {
      return NotFound($"No item with id {productId} found in cart for user with id: ${userId}");
    }

    cart.Items.Remove(itemToRemove);
    var updatedCart = await _cartRepository.UpdateCartAsync(cart);

    return Ok(updatedCart);
  }

  [MapToApiVersion("1.0")]
  [HttpPut("{userId}/items")]
  public async Task<IActionResult> AddOrUpdateCartItems(string userId, AddCartItemDto addCartItemDto)
  {
    var cart = await _cartRepository.GetCartByUserIdAsync(userId);

    if (cart is null)
    {
      var newCart = new Cart
      {
        UserId = userId,
        Items = new() { addCartItemDto.ToCartItem() }
      };

      var createdCart = await _cartRepository.CreateCartAsync(newCart);
      return Ok(createdCart);
    }

    var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == addCartItemDto.ProductId);

    if (cartItem is null)
    {
      cart.Items.Add(addCartItemDto.ToCartItem());
    }
    else
    {
      cartItem.Count += addCartItemDto.Count;
    }

    var updatedCart = await _cartRepository.UpdateCartAsync(cart);
    return Ok(updatedCart);
  }
}