namespace BeanVault.Services.ProductService.API.Dtos;

public class ProductDto
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public double Price { get; set; }
  public string Description { get; set; } = string.Empty;
  public string CategoryName { get; set; } = string.Empty;
  public string ImageUrl { get; set; } = string.Empty;

  public ProductDto()
  {
  }

  public ProductDto(Product product)
  {
    Id = product.Id;
    Name = product.Name;
    Price = product.Price;
    Description = product.Description;
    CategoryName = product.CategoryName;
    ImageUrl = product.ImageUrl;
  }
}