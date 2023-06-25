using System.Net.Http.Headers;

using Microsoft.AspNetCore.Authentication;

namespace BeanVault.Services.CartService.API.Handlers;

public class AuthHttpClientHandler : DelegatingHandler
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public AuthHttpClientHandler(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
  {
    if (_httpContextAccessor.HttpContext is not null)
    {
      var authToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
      request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
    }

    return await base.SendAsync(request, token);
  }
}