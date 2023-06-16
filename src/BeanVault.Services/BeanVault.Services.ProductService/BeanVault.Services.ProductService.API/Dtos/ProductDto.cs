namespace BeanVault.Services.ProductService.API.Dtos;

public class ProductDto
{
  [Required]
  public string Id { get; set; } = string.Empty;

  [Required]
  [MaxLength(150)]
  public string Name { get; set; } = string.Empty;

  [Required]
  [Range(0, double.MaxValue)]
  public double Price { get; set; }

  [Required]
  public string Description { get; set; } = string.Empty;

  [Required]
  [MaxLength(150)]
  public string CategoryName { get; set; } = string.Empty;

  [Required]
  [MaxLength(150)]
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

  public Product ToProduct()
  {
    return new Product
    {
      Id = Id,
      Name = Name,
      Price = Price,
      Description = Description,
      CategoryName = CategoryName,
      ImageUrl = ImageUrl,
    };
  }
}