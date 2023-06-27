namespace BeanVault.Services.CartService.API.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/carts")]
[Produces("application/json")]
public class CartsController : ControllerBase
{
  private readonly ICartRepository _cartRepository;
  private readonly ICouponService _couponService;
  private readonly IProductService _productService;

  public CartsController(
    ICartRepository cartRepository,
    ICouponService couponService,
    IProductService productService
  )
  {
    _cartRepository = cartRepository;
    _couponService = couponService;
    _productService = productService;
  }

  [MapToApiVersion("1.0")]
  [HttpGet("{userId}")]
  public async Task<IActionResult> GetCart(string userId)
  {
    var cart = await _cartRepository.GetCartByUserIdAsync(userId);

    if (cart is null)
    {
      return NotFound($"No cart found for user with id: {userId}");
    }

    var cartItemDtos = new List<CartItemDto>();

    foreach (var item in cart.Items)
    {
      var cartItemDto = await _productService.GetProductAsync(item.ProductId);

      if (cartItemDto is null)
      {
        continue;
      }

      cartItemDto.Count = item.Count;
      cartItemDto.ProductId = item.ProductId;
      cartItemDtos.Add(cartItemDto);
    }

    var cartDto = new CartDto(cart, cartItemDtos);

    if (string.IsNullOrWhiteSpace(cart.CouponCode) is false)
    {
      var couponCode = await _couponService.GetCouponAsync(cart.CouponCode);

      if (couponCode is not null && couponCode.MinAmount < cartDto.Total)
      {
        cartDto.DiscountAmount = couponCode.DiscountAmount;
        cartDto.DiscountedTotal = cartDto.Total - couponCode.DiscountAmount;
      }
    }

    return Ok(cartDto);
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