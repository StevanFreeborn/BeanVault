namespace BeanVault.Services.ProductService.Core.Models;

public class Product
{
  public string Id { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public double Price { get; set; }
  public string Description { get; set; } = string.Empty;
  public string CategoryName { get; set; } = string.Empty;
  public string ImageUrl { get; set; } = string.Empty;
}