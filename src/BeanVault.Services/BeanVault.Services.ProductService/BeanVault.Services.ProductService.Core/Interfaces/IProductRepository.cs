namespace BeanVault.Services.ProductService.Core.Interfaces;

public interface IProductRepository
{
  Task<Product> AddProductAsync(Product coupon);
  Task<List<Product>> GetProductsAsync(ProductQuery query);
  Task<Product> GetProductByIdAsync(string id);
  // Task<Product> UpdateProductByIdAsync(Product product);
  // Task DeleteProductByIdAsync(string id);
}