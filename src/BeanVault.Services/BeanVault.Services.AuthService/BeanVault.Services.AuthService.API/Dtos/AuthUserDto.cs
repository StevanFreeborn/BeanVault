namespace BeanVault.Services.AuthService.API.Dtos;

public class AuthUserDto
{
  public UserDto User { get; set; } = new UserDto();
  public IList<string> Roles { get; set; } = new List<string>();
  public string Token { get; set; } = string.Empty;
  public DateTime Expiration { get; set; } = DateTime.MinValue;

  public AuthUserDto()
  {
  }

  public AuthUserDto(ApplicationUser user, string token, DateTime tokenExpiration, IList<string> roles)
  {
    User = new UserDto(user);
    Token = token;
    Expiration = tokenExpiration;
    Roles = roles;
  }
}