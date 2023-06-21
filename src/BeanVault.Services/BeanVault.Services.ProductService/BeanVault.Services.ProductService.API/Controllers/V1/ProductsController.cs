namespace BeanVault.Services.ProductService.API.Controllers;

[Authorize]
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

  /// <summary>
  /// Get products
  /// </summary>
  [MapToApiVersion("1.0")]
  [HttpGet]
  [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetProducts([FromQuery] ProductQuery query)
  {
    var products = await _productRepository.GetProductsAsync(query);
    var productDtos = products.Select(p => new ProductDto(p)).ToList();
    return Ok(productDtos);
  }

  /// <summary>
  /// Get a product by id
  /// </summary>
  [MapToApiVersion("1.0")]
  [HttpGet("{id}")]
  [ProducesResponseType(typeof(List<ProductDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetProductById(string id)
  {
    var product = await _productRepository.GetProductByIdAsync(id);
    return Ok(new ProductDto(product));
  }

  /// <summary>
  /// Add a product
  /// </summary>
  [Authorize(Roles = "admin")]
  [MapToApiVersion("1.0")]
  [HttpPost]
  [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
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

  /// <summary>
  /// Update a product by id
  /// </summary>
  [Authorize(Roles = "admin")]
  [MapToApiVersion("1.0")]
  [HttpPut]
  [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> UpdateProduct(ProductDto productDto)
  {
    var updatedProduct = await _productRepository.UpdateProductByIdAsync(productDto.ToProduct());
    return Ok(new ProductDto(updatedProduct));
  }

  /// <summary>
  /// Delete a product by id
  /// </summary>
  [Authorize(Roles = "admin")]
  [MapToApiVersion("1.0")]
  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> DeleteProductById(string id)
  {
    await _productRepository.DeleteProductByIdAsync(id);
    return NoContent();
  }
}