namespace BeanVault.Services.CartService.API.Services;

public class ProductService : IProductService
{
  private readonly ServiceUrls _serviceUrls;
  private readonly HttpClient _client;

  public ProductService(IOptions<ServiceUrls> options, IHttpClientFactory httpClientFactory)
  {
    _serviceUrls = options.Value;
    _client = httpClientFactory.CreateClient("authClient");
    _client.BaseAddress = new Uri(_serviceUrls.ProductService);
  }

  public async Task<CartItemDto?> GetProductAsync(string id)
  {
    return await _client.GetFromJsonAsync<CartItemDto>(
      $"{_serviceUrls.ProductService}/api/products/{id}"
    );
  }
}