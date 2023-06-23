namespace BeanVault.Services.CartService.API.Dtos;

public class CartDetailsDto
{
  public string Id { get; set; } = string.Empty;
  public string CartHeaderId { get; set; } = string.Empty;
  public string ProductId { get; set; } = string.Empty;
  public int Count { get; set; }
}