namespace BeanVault.Services.ProductService.API.Dtos;

public class ProductDto
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public double Price { get; set; }
  public string Description { get; set; } = string.Empty;
  public string CategoryName { get; set; } = string.Empty;
  public string ImageUrl { get; set; } = string.Empty;
}