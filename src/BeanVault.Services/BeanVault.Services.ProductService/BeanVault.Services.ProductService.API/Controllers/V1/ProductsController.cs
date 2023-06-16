namespace BeanVault.Services.ProductService.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/products")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
  private readonly IProductRepository _productRepository;

  public ProductsController(IProductRepository productRepository)
  {
    _productRepository = productRepository;
  }

  [MapToApiVersion("1.0")]
  [HttpGet]
  [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetProducts([FromQuery] ProductQuery query)
  {
    var products = await _productRepository.GetProductsAsync(query);
    var productDtos = products.Select(p => new ProductDto(p)).ToList();
    return Ok(productDtos);
  }
}