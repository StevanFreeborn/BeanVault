namespace BeanVault.Services.ProductService.API.Dtos;

public class AddProductDto
{
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

  public Product ToProduct()
  {
    return new Product
    {
      Name = Name,
      Price = Price,
      Description = Description,
      CategoryName = CategoryName,
      ImageUrl = ImageUrl,
    };
  }
}