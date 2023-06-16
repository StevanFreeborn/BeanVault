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

  [MapToApiVersion("1.0")]
  [HttpGet("{id}")]
  [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetProductById(string id)
  {
    var product = await _productRepository.GetProductByIdAsync(id);
    return Ok(new ProductDto(product));
  }


  [MapToApiVersion("1.0")]
  [HttpPost]
  public async Task<IActionResult> AddProduct(AddProductDto addProductDto)
  {
    var product = addProductDto.ToProduct();
    var newProduct = await _productRepository.AddProductAsync(product);
    return CreatedAtAction(
      nameof(GetProductById),
      new { id = newProduct.Id },
      new ProductDto(newProduct)
    );
  }
}