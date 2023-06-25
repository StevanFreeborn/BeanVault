namespace BeanVault.Services.CartService.API.Dtos;

public class CartItemDto
{
  public string ProductId { get; set; } = string.Empty;
  public int Count { get; set; }
  public string Name { get; set; } = string.Empty;
  public double Price { get; set; }
  public string Description { get; set; } = string.Empty;
  public string CategoryName { get; set; } = string.Empty;
  public string ImageUrl { get; set; } = string.Empty;
  public double SubTotal => Count * Price;
}