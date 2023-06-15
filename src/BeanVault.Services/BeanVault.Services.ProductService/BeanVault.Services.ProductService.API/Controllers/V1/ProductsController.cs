
namespace BeanVault.Services.ProductService.API.Controllers;

public class ProductsController : ControllerBase
{
  private readonly IProductRepository _productRepository;

  public ProductsController(IProductRepository productRepository)
  {
    _productRepository = productRepository;
  }
}